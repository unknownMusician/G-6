using G6.Characters.Player;
using System;
using UnityEngine;

namespace G6.Data.SaveLoad {
    public static class SaveAndLoadGame {
        public static SaveLoad SaveLoad;

        private static void CheckSaveLoad() {
            if (SaveLoad == null) { SaveLoad = new SaveLoad("save.dat"); }
        }

        public static void SaveGame() {
            CheckSaveLoad();
            SaveLoadData data = new SaveLoadData() {
                Level = MainData.Level,
                PlayerData = PlayerBehaviour.Serialization.Real2Serializable(MainData.PlayerBehaviour)
            };

            SaveLoad.Serialize(data);
            Debug.Log("Game was Saved");
        }
        public static void LoadGame() {
            CheckSaveLoad();
            SaveLoadData data = SaveLoad.Deserialize();
            // todo: make "Load Button" inactive when saveGame file is missing;
            if (data != null)
            {
                MainData.Level = data.Level;
                BetweenScenes.Player.PlayerData = data.PlayerData;
                Debug.Log("Game was Loaded");
                LevelManager.LoadHub();
            }
        }
    }

    public class SaveLoad : BaseSaveLoad {
        public SaveLoad(string path) : base(path) { }
    }

    [Serializable]
    public class SaveLoadData {
        public int Level { get; set; }
        public PlayerBehaviour.Serialization PlayerData { get; set; }
    }
}