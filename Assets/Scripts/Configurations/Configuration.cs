﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Configuration
{
    public static string _globalConfigPath = "Global.config";
    public static string _weaponConfigPath = "Weapon.config";

    public static GlobalConfig Global;
    public static WeaponConfig weapon;

    public static void Initialize()
    {
        Global = new GlobalConfig(_globalConfigPath);
        weapon = new WeaponConfig(_weaponConfigPath);
    }
}