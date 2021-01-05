using G6.Data;
using G6.RoomSpawning;
using UnityEngine;

namespace G6.EnvironmentBuilder {
    public sealed class PlayTest : MonoBehaviour {

        public int PlayTestStartDir { get; set; } = default;

        public void Play() {
            RoomCreator.instance.SaveRoomTmp();
            BetweenScenes.EnvironmentBuilder.WhereToStart = BuilderUI.instance.PlayTestSide;
            needToStartPlayTest = true;
            LevelManager.LoadPlayTest();
            // todo
        }

        private static bool needToStartPlayTest = false;

        private Room Room { get; set; }

        private void Awake() {
            if (!needToStartPlayTest) { return; }
            if (!System.IO.File.Exists("Assets/Resources/Prefabs/RoomSpawning/Rooms/Rooms/Room_tmp.prefab")) {
                print("Error in file"); // todo
                LevelManager.LoadEnvironmentBuilder();
                return;
            }
            var roomPrefab = Resources.Load<GameObject>("Prefabs/RoomSpawning/Rooms/Rooms/Room_tmp");
            if (roomPrefab == null) {
                print("Error in GameObject"); // todo
                LevelManager.LoadEnvironmentBuilder();
                return;
            }
            var roomObj = UnityEditor.PrefabUtility.InstantiatePrefab(roomPrefab) as GameObject;
            roomObj.transform.position = Vector2.zero;
            Room = roomObj.GetComponent<Room>();
        }

        private void Start() {
            if (!needToStartPlayTest) { return; }
            needToStartPlayTest = false;
            var side = BetweenScenes.EnvironmentBuilder.WhereToStart;

            var playerTransform = MainData.PlayerBehaviour.transform;
            switch (side) {
                case Enums.Side.Down:
                    playerTransform.position = Room.BottomSpawnpoint.position;
                    break;
                case Enums.Side.Left:
                    playerTransform.position = Room.LeftSpawnpoint.position;
                    break;
                case Enums.Side.Right:
                    playerTransform.position = Room.RightSpawnpoint.position;
                    break;
                case Enums.Side.Up:
                    playerTransform.position = Room.TopSpawnpoint.position;
                    break;
                default:
                    playerTransform.position = Vector2.one * 20; // todo
                    break;
            }
        }

        public void ReturnToBuilder() => LevelManager.LoadEnvironmentBuilder();
    }
}