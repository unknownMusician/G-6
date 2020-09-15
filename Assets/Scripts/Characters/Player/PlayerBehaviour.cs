using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : CharacterBase
{

    public override float HP
    {
        get => _hp;
        protected set
        {
            _hp = value > MaxHP ? MaxHP : (value < 0 ? 0 : value);
            MainData.ActionHPChange?.Invoke();
        }
    }
    public override float SP
    {
        get => _sp;
        protected set
        {
            _sp = value > MaxSP ? MaxSP : (value < 0 ? 0 : value);
            MainData.ActionSPChange?.Invoke();
        }
    }
    public override float OP
    {
        get => _op;
        protected set
        {
            _op = value > MaxOP ? MaxOP : (value < 0 ? 0 : value);
            MainData.ActionOPChange?.Invoke();
        }
    }

    public override bool Interact(GameObject whoInterracted) {
        throw new NotImplementedException(); // todo
    }

    private void Awake()
    {
        // Setting Player to MainData
        MainData.PlayerObject = gameObject;
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

    private new void Update() 
    {
        base.Update();
        if (IsMoving)
            Move(MainData.Controls.Player.Move.ReadValue<Vector2>());
    }
}
