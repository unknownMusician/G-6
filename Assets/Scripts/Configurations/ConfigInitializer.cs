using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConfigInitializer
{
    public static string _globalConfigPath = "Global.config";

    public static GlobalConfig Global;

    public static void Initialize()
    {
        Global = new GlobalConfig(_globalConfigPath);
    }
}
