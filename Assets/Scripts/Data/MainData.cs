using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainData
{
    #region Invertary Cards
    public static Action ActionInventoryCards;
    private static List<Card> inventoryCards;

    public static List<Card> InventoryCards
    {
        get => inventoryCards;
        set
        {
            inventoryCards = value;
            ActionWeapons?.Invoke();
        }
    }

    #endregion

    #region Weapons
    public static Action ActionWeapons;
    private static List<Weapon> inventoryWeapons;
    public static Action ActionActiveWeapon;
    private static int activeWeaponIndex = 0;
    //
    private static GameObject player;

    public static GameObject Player { get => player; set => player = value; }
    public static PlayerBehaviour PlayerBehaviour => Player?.GetComponent<PlayerBehaviour>();
    public static Inventory Inventory => Player?.transform.GetChild(0)?.gameObject.GetComponent<Inventory>();
    public static Weapon ActiveWeapon => Inventory?.Weapon;
    //
    public static Action ActionPlayerPosition;
    public Vector3 PlayerPosition { get => Player.transform.position; }
    //

    public static List<Weapon> InventoryWeapons
    {
        get => inventoryWeapons;
        set
        {
            inventoryWeapons = value;
            ActionWeapons?.Invoke();
        }
    }

    public static int ActiveWeaponIndex
    {
        get => activeWeaponIndex;
        set
        {
            if (activeWeaponIndex != value)
            {
                activeWeaponIndex = value;
                ActionActiveWeapon?.Invoke();
            }
        }
    }

    #endregion

    #region HP
    public static Action ActionHP;
    private static int overallHP = 100;
    private static int currentHP = 100;

    public static int OverallHP
    {
        get
        {
            return overallHP;
        }
        set
        {
            overallHP = value;
            ActionHP?.Invoke();
        }
    }
    public static int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
            ActionHP?.Invoke();
        }
    }
    #endregion

    #region Stamina
    public static Action ActionSP;
    private static int overallSP = 100;
    private static int currentSP = 100;

    public static int OverallSP
    {
        get
        {
            return overallSP;
        }
        set
        {
            overallSP = value;
            ActionSP?.Invoke();
        }
    }
    public static int CurrentSP
    {
        get
        {
            return currentSP;
        }
        set
        {
            currentSP = value;
            ActionSP?.Invoke();
        }
    }
    #endregion

    #region XP
    public static Action ActionXP;
    private static int overallXP = 100;
    private static int currentXP = 0;

    public static int OverallXP
    {
        get
        {
            return overallXP;
        }
        set
        {
            overallXP = value;
            ActionXP?.Invoke();
        }
    }
    public static int CurrentXP
    {
        get
        {
            return currentXP;
        }
        set
        {
            currentXP = value;
            ActionXP?.Invoke();
        }
    }
    #endregion

    #region Level
    public static Action ActionLevel;
    private static int currentLevel = 0;

    public static int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            ActionLevel?.Invoke();
        }
    }

    #endregion

    #region Money
    public static Action ActionMoney;
    private static int currentMoney = 0;

    public static int CurrentMoney
    {
        get
        {
            return currentMoney;
        }
        set
        {
            currentMoney = value;
            ActionMoney?.Invoke();
        }
    }

    #endregion

    //TODO start data
    #region Position
    private static Vector3 currentPosition;

    public static Vector3 CurrentPosition
    {
        get
        {
            return currentPosition;
        }
        set
        {
            currentPosition = value;
            ActionPlayerPosition?.Invoke();
        }
    }
    #endregion
}
