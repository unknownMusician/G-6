using G6.Data;
using G6.RoomSpawning;
using UnityEngine;

namespace G6.Environment {
    public sealed class PlayTest : MonoBehaviour {

        private Room Room { get; set; }

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
            Room = Instantiate(roomObj, Vector2.zero, Quaternion.identity).GetComponent<Room>();
        }

        private void Start() {
            var side = BetweenScenes.EnvironmentBuilder.WhereToStart;
            print("2" + side); // todo
            print(Room.BottomSpawnpoint); // todo
            print(Room.LeftSpawnpoint); // todo
            print(Room.RightSpawnpoint); // todo
            print(Room.TopSpawnpoint); // todo

            var playerTransform = MainData.PlayerBehaviour.transform;
            switch (side) { // todo: check spawnPoints
                case Enums.Side.Down:
                    playerTransform.position = Room.BottomSpawnpoint.position;
                    break;
                case Enums.Side.Left:
                    playerTransform.position = Room.LeftSpawnpoint.position;
                    break;
                case Enums.Side.Right:
                    playerTransform.position = Room.RightSpawnpoint.position;
                    break;
                default: // case Enums.Side.Up:
                    playerTransform.position = Room.TopSpawnpoint.position;
                    break;
            }
            UI.Pause.GameIsPaused = false;
        }

        public void ReturnToBuilder() => LevelManager.LoadEnvironmentBuilder();
    }
}