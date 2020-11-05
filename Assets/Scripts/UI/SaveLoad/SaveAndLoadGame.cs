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
        //название файла
        private readonly string Path = System.DateTime.Now.Day + System.DateTime.Now.Millisecond.ToString();

        public static SaveLoad SaveLoad;

        public void SaveGame()
        {


            Debug.Log("Game was Saved");
            SaveLoad = new SaveLoad( /*//TODO название фала для сохранения*/Path + ".dat");
            SaveLoadData data = new SaveLoadData()
            {
                Level = MainData.Level,
                PlayerData = PlayerBehaviour.Serialization.Real2Serializable(MainData.PlayerBehaviour)
            };

            SaveLoad.Serialize(data);
            Debug.Log("Game was Saved");
        }
        public void LoadGame()
        {
            // todo: make "Load Button" inactive when saveGame file is missing;
            SaveLoadData data = SaveLoad.Deserialize();
            Debug.Log("Pressed Load");
            //MainData.Inventory = SaveLoad.Data.inventory;
            if (data != null)
            {
                MainData.Level = data.Level;
                PlayerBehaviour.Serialization.Serializable2Real(data.PlayerData, MainData.PlayerBehaviour);
                Debug.Log("Game was Loaded");
                //TODO выгрузка сохранений
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
        public int Level { get; set; }
        public PlayerBehaviour.Serialization PlayerData { get; set; }
    }
}