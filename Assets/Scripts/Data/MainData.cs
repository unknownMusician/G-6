using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainData
{
    #region Patrons
    public static Action PatronsAction;
    private static int overallPatrons;
    private static int clipPatrons;

    public static int OverallPatrons
    {
        get
        {
            return overallPatrons;
        }
        set
        {
            overallPatrons = value;
            PatronsAction();
        }
    }
    public static int ClipPatrons
    {
        get
        {
            return clipPatrons;
        }
        set
        {
            clipPatrons = value;
            PatronsAction();
        }
    }
    #endregion

    #region HP
    public static Action ActionHP;
    private static int overallHP;
    private static int currentHP;

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

    #region XP
    public static Action ActionXP;
    private static int overallXP;
    private static int currentXP;

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
    private static int currentLevel;

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
