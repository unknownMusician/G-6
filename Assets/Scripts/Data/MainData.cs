﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainData
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

    public static Inventory Inventory => PlayerObject?.GetComponentInChildren<Inventory>();
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
