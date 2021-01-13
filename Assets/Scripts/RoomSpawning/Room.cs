using Assets.Scripts.Enums;
using UnityEngine;

namespace G6.RoomSpawning {
    public class Room : MonoBehaviour {

        #region Properties

        public RoomType RoomType { get; set; }

        public string RoomStuff { get; set; }

        #region Doors

        private Door topDoor;
        private Door rightDoor;
        private Door bottomDoor;
        private Door leftDoor;

        #endregion

        #region Spawnpoints

        public Transform TopSpawnpoint { get; set; }
        public Transform RightSpawnpoint { get; set; }
        public Transform BottomSpawnpoint { get; set; }
        public Transform LeftSpawnpoint { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Awake() method

        private void OnEnable() {

            RoomType = RoomType.regular;

            #region Doors initialization

            Transform doorsCollectionObject = this.transform.GetChild(1);
            int amountOfDoors = doorsCollectionObject.childCount;
            for (int i = 0; i < amountOfDoors; i++) {
                GameObject door = doorsCollectionObject.GetChild(i).gameObject;
                if (door.name == "TopDoor") {
                    topDoor = door.GetComponent<Door>();
                } else if (door.name == "RightDoor") {
                    rightDoor = door.GetComponent<Door>();
                } else if (door.name == "BottomDoor") {
                    bottomDoor = door.GetComponent<Door>();
                } else if (door.name == "LeftDoor") {
                    leftDoor = door.GetComponent<Door>();
                }
            }

            #endregion

            #region Spawnoints initializataion

            Transform spawnpointsCollectionObject = this.transform.GetChild(3);
            int amountOfSpawnpoints = spawnpointsCollectionObject.childCount;
            for (int i = 0; i < amountOfSpawnpoints; i++) {
                GameObject spawnpoint = spawnpointsCollectionObject.GetChild(i).gameObject;
                if (spawnpoint.name == "TopSpawnpoint") {
                    TopSpawnpoint = spawnpoint.transform;
                } else if (spawnpoint.name == "RightSpawnpoint") {
                    RightSpawnpoint = spawnpoint.transform;
                } else if (spawnpoint.name == "BottomSpawnpoint") {
                    BottomSpawnpoint = spawnpoint.transform;
                } else if (spawnpoint.name == "LeftSpawnpoint") {
                    LeftSpawnpoint = spawnpoint.transform;
                }
            }

            #endregion

        }

        #endregion

        public bool isThereAnyEnemy() {
            Transform transform = GetComponent<Transform>();
            return transform.GetChild(2).childCount != 0;
        }

        public void makeAllDoorsUnvisited() {
            if (topDoor != null) {
                topDoor.Visited = false;
            }
            if (rightDoor != null) {
                rightDoor.Visited = false;
            }
            if (bottomDoor != null) {
                bottomDoor.Visited = false;
            }
            if (leftDoor != null) {
                leftDoor.Visited = false;
            }
        }

        #endregion

    }
}
