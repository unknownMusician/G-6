using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainData
{
    #region Main GameObjects

    private static GameObject player;

    public static GameObject Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
            ActionPlayerChange?.Invoke();
        }
    }
    /// <summary>
    /// Calls only on the start of each scene;
    /// </summary>
    public static Action ActionPlayerChange = ActionPlayerCoinsChange + ActionPlayerPositionChange + ActionHPChange + ActionXPChange + ActionSPChange + ActionOPChange;

    public static GameObject RoomSpawner { get; set; }

    #endregion

    #region Player

    public static PlayerBehaviour PlayerBehaviour => Player?.GetComponent<PlayerBehaviour>();

    //

    public static Action ActionPlayerPositionChange;
    public static Vector3 PlayerPosition { get => Player.transform.position; }

    private static int coins = 5;
    // To-Do: add coins to PlayerBehaviour;
    public static Action ActionPlayerCoinsChange;
    public static int PlayerCoins {
        get => coins;
        set {
            coins = value;
            ActionPlayerCoinsChange?.Invoke();
        }
    }

    private static float xp;
    private static float maxXp;
    // To-Do: add xp to PlayerBehaviour;
    public static Action ActionXPChange;
    public static float PlayerXP {
        get => xp;
        set {
            xp = value;
            ActionXPChange?.Invoke();
        }
    }
    public static float PlayerMaxXP {
        get => maxXp;
        set {
            maxXp = value;
            ActionXPChange?.Invoke();
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

    // To-Do: add NORMAL inventoryCards to Inventory;
    public static Inventory Inventory => Player?.transform.GetChild(0)?.gameObject.GetComponent<Inventory>();
    public static Weapon ActiveWeapon => Inventory?.Weapon;

    public static Action ActionInventoryCardsChange;
    public static Action ActionInventoryWeaponsChange;
    public static Action ActionInventoryActiveSlotChange;

    public static Action ActionGunBulletsChange;

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
}
