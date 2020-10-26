using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainData : MonoBehaviour {
    #region Main GameObjects

    private static GameObject _playerObject;

    public static GameObject PlayerObject {
        get => _playerObject;
        set {
            _playerObject = value;
            ActionPlayerChange?.Invoke();
        }
    }
    /// <summary> Calls only on the start of each scene </summary>
    public static Action ActionPlayerChange = 
        ActionPlayerCoinsChange + 
        ActionPlayerPositionChange + 
        ActionHPChange + ActionSPChange + 
        ActionOPChange + 
        (() => { PlayerBehaviour = PlayerObject.GetComponent<PlayerBehaviour>(); });

    private static GameObject _roomSpawnerObject;
    public static GameObject RoomSpawnerObject {
        get => _roomSpawnerObject;
        set {
            _roomSpawnerObject = value;
            ActionRoomSpawnerChange?.Invoke();
        }
    }
    public static Action ActionRoomSpawnerChange = 
        (() => { RoomSpawner = RoomSpawnerObject.GetComponent<RoomSpawner>(); });

    #endregion

    #region Player

    public static PlayerBehaviour PlayerBehaviour { get; private set; }

    //

    public static Action ActionPlayerPositionChange;
    public static Vector3 PlayerPosition { get => PlayerObject.transform.position; }

    private static int _playerCoins = 5;
    public static Action ActionPlayerCoinsChange;
    public static int PlayerCoins {
        get => _playerCoins;
        set {
            _playerCoins = value;
            ActionPlayerCoinsChange?.Invoke();
        }
    }

    public static float PlayerHP => PlayerBehaviour.HP;
    public static float PlayerMaxHP => PlayerBehaviour.MaxHP;
    public static Action ActionHPChange;
    public static float PlayerSP => PlayerBehaviour.SP;
    public static float PlayerMaxSP => PlayerBehaviour.MaxSP;
    public static Action ActionSPChange;
    public static float PlayerOP => PlayerBehaviour.OP;
    public static float PlayerMaxOP => PlayerBehaviour.MaxOP;
    public static Action ActionOPChange;

    #endregion

    #region Inventory & Guns

    public static Inventory Inventory => PlayerBehaviour.Inventory;
    public static Weapon ActiveWeapon => Inventory?.Weapon;

    public static Action ActionInventoryCardsChange;
    public static Action ActionInventoryWeaponsChange;
    public static Action ActionInventoryActiveSlotChange;

    public static Action ActionGunBulletsChange;

    #endregion

    #region RoomSpawner

    public static RoomSpawner RoomSpawner { get; private set; }

    #endregion

    #region Level

    private static int level = 1;
    // To-Do: add level to MainData;
    public static Action ActionLevelChange;
    public static int Level {
        get => level;
        set {
            level = value;
            ActionLevelChange?.Invoke();
        }
    }

    #endregion

    #region UI

    public static GameUI GameUI { get; set; }

    #endregion

    #region Input

    public static InputMaster Controls { get; private set; }

    private void SetControlsActions() {

        #region Weapon

        Controls.Weapon.AttackPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackStart(); };
        Controls.Weapon.AttackRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackEnd(); };
        Controls.Weapon.ChangeWeaponState.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ChangeWeaponState(); };
        Controls.Weapon.Reload.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ReloadGun(); };
        Controls.Weapon.ThrowPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ThrowPress(); };
        Controls.Weapon.ThrowRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ThrowRelease(); };
        Controls.Weapon.Slot1.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.FIRST; };
        Controls.Weapon.Slot2.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.SECOND; };
        Controls.Weapon.Slot3.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.THIRD; };
        Controls.Weapon.Slot4.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.FOURTH; };
        Controls.Weapon.ChangeSlot.performed += ctx => {
            if (!Pause.GameIsPaused)
                _ = Mouse.current.scroll.ReadValue().y < 0 ? Inventory.ActiveSlot-- : Inventory.ActiveSlot++;
        };
        Controls.Weapon.AimMouse.performed += ctx => {
            if (!Pause.GameIsPaused) {
                Inventory.Aim(ctx.ReadValue<Vector2>(), Inventory.CoordsType.Screen); // Weapon
            }
        };
        Controls.Weapon.AimStick.performed += ctx => {
            if (!Pause.GameIsPaused) {
                Inventory.Aim(ctx.ReadValue<Vector2>(), Inventory.CoordsType.Local); // Weapon
            }
        };

        #endregion

        #region Player

        Controls.Player.Jump.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.Jump(); };

        Controls.Player.Sneak.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsCrouching = true; };
        Controls.Player.NoSneak.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsCrouching = false; };

        Controls.Player.Run.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsRunning = true; };
        Controls.Player.NoRun.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsRunning = false; };
        Controls.Player.RunChange.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsRunning = !PlayerBehaviour.IsRunning; };

        Controls.Player.Interact.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.TryInteract(); };

        Controls.Player.Move.performed += ctx => { PlayerBehaviour.IsMoving = true; };
        Controls.Player.NoMove.performed += ctx => { PlayerBehaviour.IsMoving = false; };

        #endregion

        #region UI

        Controls.UI.Menu.performed += ctx => {
            Pause.GameIsPaused = !Pause.GameIsPaused;
            GameUI.menu.SetActive(Pause.GameIsPaused);
        };

        Controls.UI.WeaponSettings.performed += ctx => {
            Pause.GameIsPaused = !Pause.GameIsPaused;
            GameUI.weaponSettings.SetActive(Pause.GameIsPaused);
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
        Controls = new InputMaster();
        SetControlsActions();
    }
    private void OnEnable() {
        Controls.Enable();
    }
    private void OnDisable() {
        Controls.Disable();
    }

    #endregion
}
