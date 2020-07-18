using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainData
{
    #region Main GameObjects

    public static GameObject Player { get; set; }
    public static GameObject RoomSpawner { get; set; }

    #endregion

    #region Player

    public static PlayerBehaviour PlayerBehaviour => Player?.GetComponent<PlayerBehaviour>();

    //

    public static Action ActionPlayerPosition;
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

    private static int xp;
    private static int maxXp;
    // To-Do: add xp to PlayerBehaviour;
    public static Action ActionXPChange;
    public static int XP {
        get => xp;
        set {
            xp = value;
            ActionXPChange?.Invoke();
        }
    }
    public static int MaxXP {
        get => maxXp;
        set {
            maxXp = value;
            ActionXPChange?.Invoke();
        }
    }

    public float PlayerHP => PlayerBehaviour.HP;
    public float PlayerMaxHP => PlayerBehaviour.MaxHP;
    public float PlayerSP => PlayerBehaviour.SP;
    public float PlayerMaxSP => PlayerBehaviour.MaxSP;
    public float PlayerOP => PlayerBehaviour.OP;
    public float PlayerMaxOP => PlayerBehaviour.MaxOP;

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
