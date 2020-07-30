using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.UI.SaveLoad;
using UnityEngine;

public class SaveAndLoadGame : MonoBehaviour
{
    private static string path = DataSetDateTime.Local.ToString();

    public static SaveLoad SaveLoad;
    public static void SaveGame(string name)
    {
        SaveLoad = new SaveLoad(name + DataSetDateTime.Local.ToString() + ".sl");
        SaveLoad.Serialize();
    }
    public static void LoadGame(string name)
    {
        SaveLoad = new SaveLoad(name);
        SaveLoad.Deserialize();
    }
}

public class SaveLoad : BaseSaveLoad<SaveLoadData>
{
    public SaveLoad(string path) : base(path)
    {

    }
}

[Serializable]
public class SaveLoadData
{

}