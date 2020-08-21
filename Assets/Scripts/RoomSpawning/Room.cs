using UnityEngine;

public class Room : MonoBehaviour {

    #region Properties

    public byte RoomType { get; set; }

    #region Doors

    private Door topDoor;
    private Door rightDoor;
    private Door bottomDoor;
    private Door leftDoor;
    
    #endregion

    #region Spawnpoints
    
    public GameObject TopSpawnpoint { get; set; }
    public GameObject RightSpawnpoint { get; set; }
    public GameObject BottomSpawnpoint { get; set; }
    public GameObject LeftSpawnpoint { get; set; }

    #endregion

    #endregion

    #region Class constructor

    public static class TypeOfTheRoom {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }

    #endregion

    #region Methods

    #region Awake() method

    private void Awake() {

        RoomType = TypeOfTheRoom.regular;

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
                TopSpawnpoint = spawnpoint;
            } else if (spawnpoint.name == "RightSpawnpoint") {
                RightSpawnpoint = spawnpoint;
            } else if (spawnpoint.name == "BottomSpawnpoint") {
                BottomSpawnpoint = spawnpoint;
            } else if (spawnpoint.name == "LeftSpawnpoint") {
                LeftSpawnpoint = spawnpoint;
            }
        }
        
        #endregion

    }

    #endregion

    public bool isThereAnyEnemy() {
        Transform transform = GetComponent<Transform>();
        return transform.GetChild(0).childCount != 0;
    }

    public void makeAllDoorsUnvisited () {
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
