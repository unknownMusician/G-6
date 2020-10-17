using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.SaveLoad
{
    public class SaveAndLoadGame : MonoBehaviour
    {
        private readonly string Path = System.DateTime.Now.Day + System.DateTime.Now.Millisecond.ToString();

        public static SaveLoad SaveLoad;


        public InputField inputFieldSave;



        public void SaveGame()
        {


            Debug.Log("Gmae was Saved");
            SaveLoad = new SaveLoad(inputFieldSave.text + Path + ".dat");
            SaveLoadData data = new SaveLoadData()
            {
                //inventory = MainData.Inventory,
                //playerObject = MainData.PlayerObject,
                //playerBehaviour = MainData.PlayerBehaviour,
                //playerPosition = MainData.PlayerPosition,
                coins = MainData.PlayerCoins,
                level = MainData.Level,
            };

            SaveLoad.Serialize(data);
            Debug.Log("Gmae was Saved");
        }
        public void LoadGame()
        {
            SaveLoadData data = SaveLoad.Deserialize();
            Debug.Log("Pressed Load");
            //MainData.Inventory = SaveLoad.Data.inventory;
            if (data != null)
            {
                //MainData.PlayerObject = data.playerObject;
                // MainData.PlayerBehaviour = SaveLoad.Data.playerBehaviour;
                // MainData.PlayerPosition = SaveLoad.Data.playerPosition;
                MainData.PlayerCoins = data.coins;
                MainData.Level = data.level;
                Debug.Log("Gmae was Loaded");
            }
        }
    }

    public class SaveLoad : BaseSaveLoad
    {
        public SaveLoad(string path) : base(path)
        {

        }
    }

    [Serializable]
    public class SaveLoadData
    {
        //public GameObject playerObject;
        //public PlayerBehaviour playerBehaviour;
        //public Vector3 playerPosition;
        public int coins;
        //public Inventory inventory;
        public int level;
    }
}