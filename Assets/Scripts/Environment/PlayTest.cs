using G6.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace G6.Environment {
    public sealed class PlayTest : MonoBehaviour {
        private void Awake() {
            if (!System.IO.File.Exists("Assets/Resources/Prefabs/EnvironmentBuilder/Rooms/Room_tmp.prefab")) {
                print("Error in file"); // todo
                LevelManager.LoadEnvironmentBuilder();
                return;
            }
            var roomObj = Resources.Load<GameObject>("Prefabs/EnvironmentBuilder/Rooms/Room_tmp");
            if (roomObj == null) {
                print("Error in GameObject"); // todo
                LevelManager.LoadEnvironmentBuilder();
                return;
            }
            Instantiate(roomObj, Vector2.zero, Quaternion.identity);
        }

        private void Start() {
            var side = BetweenScenes.EnvironmentBuilder.WhereToStart;
            var playerTransform = MainData.PlayerBehaviour.transform;
            switch (side) {
                default: // case Enums.Side.Up:
                    playerTransform.position = new Vector2(15, 20); // todo
                    break;
            }
            Time.timeScale = 1;
        }

        public void ReturnToBuilder() => LevelManager.LoadEnvironmentBuilder();
    }
}