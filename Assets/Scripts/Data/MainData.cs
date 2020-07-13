using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MainData
{
    #region Invertary Cards
    public static Action ActionInventoryCards;
    private static ImmutableList<Card.NestedInfo> inventoryCards;

    public static ImmutableList<Card.NestedInfo> InventoryCards
    {
        get
        {
            return inventoryCards;
        }
        set
        {
            if (!inventoryCards.SequenceEqual(value)) {
                inventoryCards = value;
            }
            ActionWeapons();
        }
    }

    #endregion

    #region Weapons
    public static Action ActionWeapons;
    private static ImmutableList<Weapon.NestedInfo> inventoryWeapons;
    private static int activeWeaponIndex = 0;

    public static ImmutableList<Weapon.NestedInfo> InventoryWeapons
    {
        get => inventoryWeapons;
        set
        {
            if (!inventoryWeapons.SequenceEqual(value)) {
                inventoryWeapons = value;
                ActionWeapons();
            }
        }
    }

    public static Weapon.NestedInfo ActiveWeapon { get => inventoryWeapons?[activeWeaponIndex]; }

    public static int ActiveWeaponIndex
    {
        get => activeWeaponIndex;
        set
        {
            if (activeWeaponIndex != value) {
                activeWeaponIndex = value;
                ActionWeapons();
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
            ActionHP();
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
            ActionHP();
        }
    }
    #endregion

    #region Endurance
    public static Action ActionEndurance;
    private static int overallEndurance = 100;
    private static int currentEndurance = 100;

    public static int OverallEndurance
    {
        get
        {
            return overallEndurance;
        }
        set
        {
            overallEndurance = value;
            ActionEndurance();
        }
    }
    public static int CurrentEndurance
    {
        get
        {
            return currentEndurance;
        }
        set
        {
            currentEndurance = value;
            ActionEndurance();
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
            ActionXP();
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
            ActionXP();
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
            ActionLevel();
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
            ActionMoney();
        }
    }

    #endregion

    //TODO start data
    #region Position
    public static Action ActionPosition;
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
            ActionPosition();
        }
    }
    #endregion
}
