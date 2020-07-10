using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainData
{
    #region Weapons
    public static Action ActionWeapons;
    private static Dictionary<GameObject,List<GameObject>> inventoryWeapons;
    private static GameObject activeWeapon;

    public static List<GameObject> ActiveCards
    {
        get
        {
            return inventoryWeapons[activeWeapon];
        }
    }

    public static Dictionary<GameObject, List<GameObject>> InventoryWeapons {
        get {
            return inventoryWeapons;
        }
        set {
            inventoryWeapons = value;
            ActionWeapons();
        }
    }

    public static GameObject ActiveWeapon {
        get {
            return activeWeapon;
        }
        set {
            activeWeapon = value;
            ActionWeapons();
        }
    }

    #endregion

    #region Patrons
    public static Action ActionPatrons;
    private static int pocketBullets = 0;
    private static int clipBullets = 0;

    public static int PocketBullets
    {
        get
        {
            return pocketBullets;
        }
        set
        {
            pocketBullets = value;
            ActionPatrons();
        }
    }
    public static int ClipBullets
    {
        get
        {
            return clipBullets;
        }
        set
        {
            clipBullets = value;
            ActionPatrons();
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
