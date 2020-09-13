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
        SaveLoad.Data = new SaveLoadData()
        {
            //inventory = MainData.Inventory,
            player = MainData.Player,
            //playerBehaviour = MainData.PlayerBehaviour,
            //playerPosition = MainData.PlayerPosition,
            coins = MainData.PlayerCoins,
            level = MainData.Level,
            maxXp = MainData.PlayerMaxXP,
            xp = MainData.PlayerXP,
        };

        SaveLoad = new SaveLoad(name + DataSetDateTime.Local.ToString() + ".sl");
        SaveLoad.Serialize();
    }
    public static void LoadGame(string name)
    {
        SaveLoad = new SaveLoad(name);
        SaveLoad.Deserialize();
        
        //MainData.Inventory = SaveLoad.Data.inventory;
        MainData.Player = SaveLoad.Data.player;
       // MainData.PlayerBehaviour = SaveLoad.Data.playerBehaviour;
       // MainData.PlayerPosition = SaveLoad.Data.playerPosition;
        MainData.PlayerCoins = SaveLoad.Data.coins;
        MainData.Level = SaveLoad.Data.level;
        MainData.PlayerXP = SaveLoad.Data.xp;
        MainData.PlayerMaxXP = SaveLoad.Data.maxXp;

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
    public GameObject player;
    //public PlayerBehaviour playerBehaviour;
    //public Vector3 playerPosition;
    public int coins;
    public float xp;
    public float maxXp;
    //public Inventory inventory;
    public int level;
}