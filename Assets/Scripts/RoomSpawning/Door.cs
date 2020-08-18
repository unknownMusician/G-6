using UnityEngine;

public class Door : MonoBehaviour {

    private RoomSpawner roomSpawner;
    private GameObject[,] roomMatrix;
    private GameObject[,] miniMapMatrix;
    private GameObject spawnpoint;
    private byte direction;
    public bool Visited { get; set; }
    
    // direction - отвечает за направление в которое смотрит дверь
    // 0 - вверх
    // 1 - вправо
    // 2 - вниз
    // 3 - влево

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
        roomSpawner = this.transform.parent.parent.parent.parent.GetComponent<RoomSpawner>();
        roomMatrix = roomSpawner.getRoomsMatrix();
        miniMapMatrix = roomSpawner.getMiniMapMatrix();
        Visited = false;
    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.name == "Player") {

            Transform playerTransform = other.transform;
            GameObject currentRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn];
            GameObject currentRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn];

            if (direction == 0) {

                if (Visited == false) {
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn];
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().BottomSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    roomSpawner.CurrentRow -= 1;
                }

            } else if (direction == 1) {

                if (Visited == false) {
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1];
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().LeftSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    roomSpawner.CurrentColumn += 1;
                }

            } else if (direction == 2) {

                if (Visited == false) {
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn];
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log(nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color);
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().TopSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
                    currentRoomGameObject.SetActive(false);
                    roomSpawner.CurrentRow += 1;
                }

            } else if (direction == 3) {
                
                if (Visited == false) {
                    GameObject nextRoomGameObject = roomMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1];
                    GameObject nextRoomMiniMapElement = miniMapMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1];
                    nextRoomGameObject.SetActive(true);
                    nextRoomGameObject.GetComponent<Room>().makeAllDoorsUnvisited();
                    nextRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.red;
                    spawnpoint = nextRoomGameObject.GetComponent<Room>().RightSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                        currentRoomMiniMapElement.GetComponent<SpriteRenderer>().color = Color.white;
        currentRoomGameObject.SetActive(false);
                    roomSpawner.CurrentColumn -= 1;
                }
            }
            Visited = true;
        }
    }
}
