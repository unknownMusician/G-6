using UnityEngine;

public class Door : MonoBehaviour {

    #region Roomspawner parameter
    private RoomSpawner roomSpawner;
    #endregion

    #region Rooms and mini-map matrix parameters
    private GameObject[,] roomMatrix;
    private GameObject[,] miniMapMatrix;
    #endregion

    #region Is room visited or not parameter
    public bool Visited { get; set; }
    #endregion

    #region Direction of door parameter
    private byte direction;
    // direction - responsible for direction in which door is being directed
    // 0 - up
    // 1 - right
    // 2 - down
    // 3 - left
    #endregion

    void Awake() {
        if (this.name == "TopDoor") {
            direction = 0;
        } else if (this.name == "RightDoor") {
            direction = 1;
        } else if (this.name == "BottomDoor") {
            direction = 2;
        } else if (this.name == "LeftDoor") {
            direction = 3;
        }
    }

    void Start() {
        roomSpawner = MainData.RoomSpawnerObject.GetComponent<RoomSpawner>();
        roomMatrix = roomSpawner.getRoomsMatrix();
        miniMapMatrix = roomSpawner.getMiniMapMatrix();
        Visited = false;
    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.name == "Player") {

            Transform playerTransform = other.transform;
            GameObject currentRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn];
            GameObject currentRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn];
            GameObject spawnpoint;

            if (direction == 0) {

                if (Visited == false) {

                    #region Preparing next room
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn];
                    #endregion

                    #region Operations with new room
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    #endregion

                    #region Moving player to new room
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().BottomSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    #endregion

                    #region Focusing camera on a new room
                    float coordX = nextRoomMiniMapElement.transform.position.x;
                    float coordY = nextRoomMiniMapElement.transform.position.y;
                    float coordZ = roomSpawner.transform.GetChild(2).position.z;
                    roomSpawner.transform.GetChild(2).transform.position = new Vector3(coordX, coordY, coordZ);
                    #endregion

                    #region Operations with old room
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    #endregion

                    roomSpawner.CurrentRow -= 1;
                }

            } else if (direction == 1) {

                if (Visited == false) {

                    #region Preparing next room objects
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1];
                    #endregion

                    #region Operations with new room
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    #endregion

                    #region Moving player to new room
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().LeftSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    #endregion

                    #region Focusing camera on a new room
                    float coordX = nextRoomMiniMapElement.transform.position.x;
                    float coordY = nextRoomMiniMapElement.transform.position.y;
                    float coordZ = roomSpawner.transform.GetChild(2).position.z;
                    roomSpawner.transform.GetChild(2).transform.position = new Vector3(coordX, coordY, coordZ);
                    #endregion

                    #region Operations with old room
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    #endregion

                    roomSpawner.CurrentColumn += 1;
                }

            } else if (direction == 2) {

                if (Visited == false) {

                    #region Preparing next room objects
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn];
                    #endregion

                    #region Operations with new room
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    #endregion

                    #region Moving player to new room
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().TopSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    #endregion

                    #region Focusing camera on a new room
                    float coordX = nextRoomMiniMapElement.transform.position.x;
                    float coordY = nextRoomMiniMapElement.transform.position.y;
                    float coordZ = roomSpawner.transform.GetChild(2).position.z;
                    roomSpawner.transform.GetChild(2).transform.position = new Vector3(coordX, coordY, coordZ);
                    #endregion

                    #region Operations with old room
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    #endregion

                    roomSpawner.CurrentRow += 1;
                }

            } else if (direction == 3) {
                
                if (Visited == false) {

                    #region Preparing next room objects
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1];
                    #endregion

                    #region Operations with new room
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    #endregion

                    #region Moving player to new room
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().RightSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    #endregion

                    #region Focusing camera on a new room
                    float coordX = nextRoomMiniMapElement.transform.position.x;
                    float coordY = nextRoomMiniMapElement.transform.position.y;
                    float coordZ = roomSpawner.transform.GetChild(2).position.z;
                    roomSpawner.transform.GetChild(2).transform.position = new Vector3(coordX, coordY, coordZ);
                    #endregion

                    #region Operations with old room
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    #endregion

                    roomSpawner.CurrentColumn -= 1;
                }
            }
            Visited = true;
        }
    }
}
