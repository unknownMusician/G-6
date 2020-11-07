using G6.Data;
using G6.Weapons;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace G6.Characters.Player {
    public sealed class PlayerBehaviour : CharacterBase
    {
        public UnityAction OnHpChange { get; set; }
        public UnityAction OnSpChange { get; set; }
        public UnityAction OnOpChange { get; set; }
        public override float HP
        {
            get => _hp;
            protected set
            {
                _hp = Mathf.Clamp(value, 0, MaxHP);
                OnHpChange?.Invoke();
            }
        }
        public override float SP
        {
            get => _sp;
            protected set
            {
                _sp = Mathf.Clamp(value, 0, MaxSP);
                OnSpChange?.Invoke();
            }
        }
        public override float OP
        {
            get => _op;
            protected set
            {
                _op = Mathf.Clamp(value, 0, MaxOP);
                OnOpChange?.Invoke();
            }
        }

        private Coroutine MovingCoroutine = null;

        private new void Awake()
        {
            base.Awake();
            // loading Save
            var playerData = BetweenScenes.Player.PlayerData;
            if (playerData != null) {
                Serialization.Serializable2Real(playerData, this);
                BetweenScenes.Player.PlayerData = null;
            }
            // Setting Player to MainData
            MainData.PlayerObject = gameObject;
        }

        private void OnDestroy() {
            MainData.PlayerObject = null;
        }

        //private new void Start()
        //{
        //    CurrentEffects[CardEffect.EffectType.Fire] = new EffectControl(
        //        new CardEffect.NestedProps(
        //            CardEffect.EffectType.Fire,
        //            1,
        //            20,
        //            1
        //            ), this);
        //}

        public void Move(bool isMoving) {
            if (isMoving) {
                if (MovingCoroutine == null) {
                    MovingCoroutine = StartCoroutine(Moving());
                } else {
                    throw new Exception("You want to start moving, but you are already moving");
                }
            } else {
                if (MovingCoroutine != null) {
                    StopCoroutine(MovingCoroutine);
                    MovingCoroutine = null;
                } else {
                    throw new Exception("You want to stop moving, but there is nothing to stop");
                }
            }
        }

        private IEnumerator Moving() {
            while (true) {
                yield return new WaitForFixedUpdate();
                Move(MainData.Controls.Player.Move.ReadValue<Vector2>());
            }
        }

        [System.Serializable] public class Serialization {

            public Inventory.Serialization inventory;
        
            private Serialization(PlayerBehaviour player) {
                inventory = Inventory.Serialization.Real2Serializable(player.Inventory);
            }

            public static Serialization Real2Serializable(PlayerBehaviour player) { return new Serialization(player); }

            public static void Serializable2Real(Serialization serialization, PlayerBehaviour player) {
                Inventory.Serialization.Serializable2Real(serialization.inventory, player.GetComponentInChildren<Inventory>());
            }
        }
    }
}