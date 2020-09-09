using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MainData : MonoBehaviour
{
    #region Main GameObjects

    private static GameObject player;

    public static GameObject PlayerObject
    {
        get => player;
        set
        {
            player = value;
            ActionPlayerChange?.Invoke();
        }
    }
    /// <summary>
    /// Calls only on the start of each scene;
    /// </summary>
    public static Action ActionPlayerChange = ActionPlayerCoinsChange + ActionPlayerPositionChange + ActionHPChange + ActionSPChange + ActionOPChange;

    public static GameObject RoomSpawnerObject { get; set; }

    #endregion

    #region Player

    public static PlayerBehaviour PlayerBehaviour => PlayerObject?.GetComponent<PlayerBehaviour>();

    //

    public static Action ActionPlayerPositionChange;
    public static Vector3 PlayerPosition { get => PlayerObject.transform.position; }

    private static int coins = 5;
    public static Action ActionPlayerCoinsChange;
    public static int PlayerCoins {
        get => coins;
        set {
            coins = value;
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

    public static RoomSpawner RoomSpawner => RoomSpawnerObject.GetComponent<RoomSpawner>();

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

    #region Input

    private static InputMaster _controls;
    public static InputMaster Controls => _controls;

    #endregion

    #region Mono

    private void Awake() {
        _controls = new InputMaster();
    }
    private void OnEnable() {
        Controls.Weapon.Enable();
        Controls.Player.Enable();

        #region Controls

        #region Weapon Controls

        Controls.Weapon.AttackPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackWithWeaponOrFistPress(); };
        Controls.Weapon.AttackRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackWithWeaponOrFistRelease(); };
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
        Controls.Weapon.Aim.performed += ctx => {
            if (!Pause.GameIsPaused) {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Inventory.Aim(mouse); // Weapon
                PlayerBehaviour.Side = PlayerBehaviour.CheckSideLR(mouse); // Player
            }
        };

        #endregion

        #region Player

        Controls.Player.Jump.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.Jump(); };

        Controls.Player.Sneak.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsSneaking = true; };
        Controls.Player.Stand.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsSneaking = false; };

        Controls.Player.Run.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsRunning = true; };
        Controls.Player.Go.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.IsRunning = false; };

        Controls.Player.Interact.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.TryInteract(); };

        Controls.Player.MoveHorizontal.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.MoveX(ctx.ReadValue<float>()); };
        Controls.Player.MoveVertical.performed += ctx => { if (!Pause.GameIsPaused) PlayerBehaviour.MoveY(ctx.ReadValue<float>()); };

        Controls.Player.Stay.performed += ctx => { PlayerBehaviour.MoveY(0); PlayerBehaviour.MoveX(0); };

        #endregion

        #endregion

    }
    private void OnDisable() {
        Controls.Weapon.Disable();
        Controls.Player.Disable();
    }

    #endregion
}
