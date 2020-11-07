using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using G6.UI;
using G6.Weapons;
using G6.Characters.Player;
using G6.RoomSpawning;

namespace G6.Data {
    public sealed class MainData : MonoBehaviour {
        #region Main GameObjects

        private static GameObject _playerObject;
        public static GameObject PlayerObject {
            get {
                return _playerObject;
            }
            set {
                _playerObject = value;
                PlayerBehaviour = _playerObject?.GetComponent<PlayerBehaviour>();
            }
        }
        /// <summary> Calls only on the start of each scene </summary>
        public static UnityAction ActionPlayerChange =
            ActionPlayerCoinsChange +
            ActionHPChange + ActionSPChange + ActionOPChange;

        private static GameObject _roomSpawnerObject;
        public static GameObject RoomSpawnerObject {
            get => _roomSpawnerObject;
            set {
                _roomSpawnerObject = value;
                RoomSpawner = _roomSpawnerObject?.GetComponent<RoomSpawner>();
                ActionRoomSpawnerChange?.Invoke();
            }
        }
        public static UnityAction ActionRoomSpawnerChange;

        #endregion

        #region Player

        public static PlayerBehaviour _playerBehaviour;
        public static PlayerBehaviour PlayerBehaviour {
            get => _playerBehaviour;
            private set {
                _playerBehaviour = value;
                if (_playerObject != null) { // todo: remove
                    _playerBehaviour.OnHpChange += ActionHPChange;
                    _playerBehaviour.OnSpChange += ActionSPChange;
                    _playerBehaviour.OnOpChange += ActionOPChange;
                    ActionPlayerChange?.Invoke();
                }
            }
        }

        //

        private static int _playerCoins = 5;
        public static UnityAction ActionPlayerCoinsChange;
        public static int PlayerCoins {
            get => _playerCoins;
            set {
                _playerCoins = value;
                ActionPlayerCoinsChange?.Invoke();
            }
        }

        public static float PlayerHP => PlayerBehaviour.HP;
        public static float PlayerMaxHP => PlayerBehaviour.MaxHP;
        public static UnityAction ActionHPChange;
        public static float PlayerSP => PlayerBehaviour.SP;
        public static float PlayerMaxSP => PlayerBehaviour.MaxSP;
        public static UnityAction ActionSPChange;
        public static float PlayerOP => PlayerBehaviour.OP;
        public static float PlayerMaxOP => PlayerBehaviour.MaxOP;
        public static UnityAction ActionOPChange;

        #endregion

        #region Inventory & Guns

        public static Inventory Inventory => PlayerBehaviour?.Inventory;
        public static Weapon ActiveWeapon => Inventory?.Weapons.Weapon;

        public static UnityAction ActionInventoryCardsChange;
        public static UnityAction ActionInventoryWeaponsChange;
        public static UnityAction ActionInventoryActiveSlotChange;

        public static UnityAction ActionGunBulletsChange;

        #endregion

        #region RoomSpawner

        public static RoomSpawner RoomSpawner { get; private set; }

        #endregion

        #region Level

        private static int level = 1;
        // To-Do: add level to MainData;
        public static UnityAction ActionLevelChange;
        public static int Level {
            get => level;
            set {
                level = value;
                ActionLevelChange?.Invoke();
            }
        }

        #endregion

        #region Input

        public static InputMaster Controls { get; private set; }

        private void SetControlsActions() {

            #region Weapon

            Controls.Weapon.AttackPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.AttackStart(); };
            Controls.Weapon.AttackRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.AttackEnd(); };
            Controls.Weapon.ChangeWeaponState.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.ChangeWeaponState(); };
            Controls.Weapon.Reload.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.ReloadGun(); };
            Controls.Weapon.ThrowPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.ThrowPress(); };
            Controls.Weapon.ThrowRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory?.ThrowRelease(); };
            Controls.Weapon.Slot1.performed += ctx => { if (!Pause.GameIsPaused && Inventory != null) Inventory.Weapons.ActiveSlot = Inventory.Slots.FIRST; };
            Controls.Weapon.Slot2.performed += ctx => { if (!Pause.GameIsPaused && Inventory != null) Inventory.Weapons.ActiveSlot = Inventory.Slots.SECOND; };
            Controls.Weapon.Slot3.performed += ctx => { if (!Pause.GameIsPaused && Inventory != null) Inventory.Weapons.ActiveSlot = Inventory.Slots.THIRD; };
            Controls.Weapon.Slot4.performed += ctx => { if (!Pause.GameIsPaused && Inventory != null) Inventory.Weapons.ActiveSlot = Inventory.Slots.FOURTH; };
            Controls.Weapon.ChangeSlot.performed += ctx => {
                if (!Pause.GameIsPaused && Inventory != null)
                    _ = Mouse.current.scroll.ReadValue().y < 0 ? Inventory.Weapons.ActiveSlot-- : Inventory.Weapons.ActiveSlot++;
            };
            Controls.Weapon.AimMouse.performed += ctx => {
                if (!Pause.GameIsPaused) {
                    Inventory?.Aim(ctx.ReadValue<Vector2>(), Inventory.CoordsType.Screen); // Weapon
                }
            };
            Controls.Weapon.AimStick.performed += ctx => {
                if (!Pause.GameIsPaused) {
                    Inventory?.Aim(ctx.ReadValue<Vector2>(), Inventory.CoordsType.Local); // Weapon
                }
            };

            #endregion

            #region Player

            Controls.Player.Jump.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour?.Jump(); };

            Controls.Player.Sneak.performed += ctx => { if (!Pause.GameIsPaused && PlayerBehaviour != null) PlayerBehaviour.IsCrouching = true; };
            Controls.Player.NoSneak.performed += ctx => { if (!Pause.GameIsPaused && PlayerBehaviour != null) PlayerBehaviour.IsCrouching = false; };

            Controls.Player.Run.performed += ctx => { if (!Pause.GameIsPaused && PlayerBehaviour != null) PlayerBehaviour.IsRunning = true; };
            Controls.Player.NoRun.performed += ctx => { if (!Pause.GameIsPaused && PlayerBehaviour != null) PlayerBehaviour.IsRunning = false; };
            Controls.Player.RunChange.performed += ctx => { if (!Pause.GameIsPaused && PlayerBehaviour != null) PlayerBehaviour.IsRunning = !PlayerBehaviour.IsRunning; };

            Controls.Player.Interact.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour?.TryInteract(); };

            Controls.Player.Move.performed += ctx => { PlayerBehaviour?.Move(true); };
            Controls.Player.NoMove.performed += ctx => { PlayerBehaviour?.Move(false); };

            #endregion

            #region UI

            Controls.UI.Menu.performed += ctx => {
                Pause.GameIsPaused = !Pause.GameIsPaused;
                Menu.instance.gameObject.SetActive(Pause.GameIsPaused);
            };

            Controls.UI.WeaponSettings.performed += ctx => {
                Pause.GameIsPaused = !Pause.GameIsPaused;
                WeaponSettings.instance.gameObject.SetActive(Pause.GameIsPaused);
            };

            #endregion

        }

        public static Vector2 SquareNormalized(Vector2 v) {
            float x = (v.x == 0) ? 0 : Mathf.Sign(v.x);
            float y = (v.y == 0) ? 0 : Mathf.Sign(v.y);
            return new Vector2(x, y);
        }

        #endregion

        #region Mono

        private void Awake() {
            if (Controls == null) {
                Controls = new InputMaster();
                SetControlsActions();
            }
        }
        private void OnEnable() {
            Controls.Enable();
        }
        private void OnDisable() {
            Controls.Disable();
        }

        #endregion

        public static class Constants {
            public static readonly float gravityScale = 9.8f;
        }
    }
}