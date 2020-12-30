using G6.Data;
using UnityEngine;


namespace G6.RoomSpawning {
    public class Door : MonoBehaviour {

        #region Properties

        private RoomSpawner roomSpawner;

        private GameObject[,] roomMatrix => MainData.RoomSpawner.RoomsMatrix; // todo: remove
        private GameObject[,] miniMapMatrix => MainData.RoomSpawner.MiniMapMatrix; // todo: remove

        public bool Visited { get; set; } = false;

        private int direction;
        // direction - responsible for direction in which door is being directed
        // 0 - up
        // 1 - right
        // 2 - down
        // 3 - left

        #endregion

        #region Methods

        #region Awake() and Start() methods

        void Awake() {
            if (name == "TopDoor") {
                direction = 0;
            } else if (name == "RightDoor") {
                direction = 1;
            } else if (name == "BottomDoor") {
                direction = 2;
            } else if (name == "LeftDoor") {
                direction = 3;
            }
        }

        #endregion

        void OnTriggerEnter2D(Collider2D other) {
            if ((other.name == "Player") && (Visited == false)) {
                if (MainData.RoomSpawner != null) {
                    MainData.RoomSpawner.GoToNextRoom(other.transform, direction);
                    Visited = true;
                } else {
                    LevelManager.LoadEnvironmentBuilder();
                }
            }
        }

        #endregion

    }
}
