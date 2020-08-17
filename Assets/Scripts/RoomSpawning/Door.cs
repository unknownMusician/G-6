using UnityEngine;

public class Door : MonoBehaviour {

    private RoomSpawner roomSpawner;
    private GameObject[,] tempMatrix;
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
        tempMatrix = roomSpawner.getRoomsMatrix();
        Visited = false;
    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.name == "Player") {

            Transform playerTransform = other.transform;
            GameObject currentRoom = tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn];
            GameObject spawnpoint;

            if (direction == 0) {

                if (Visited == false) {
                    GameObject nextRoom = tempMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn];
                    nextRoom.SetActive(true);
                    nextRoom.GetComponent<Room>().makeAllDoorsUnvisited();
                    spawnpoint = nextRoom.GetComponent<Room>().BottomSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoom.SetActive(false);
                    roomSpawner.CurrentRow -= 1;
                }

            } else if (direction == 1) {

                if (Visited == false) {
                    GameObject nextRoom = tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1];
                    nextRoom.SetActive(true);
                    nextRoom.GetComponent<Room>().makeAllDoorsUnvisited();
                    spawnpoint = nextRoom.GetComponent<Room>().LeftSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoom.SetActive(false);
                    roomSpawner.CurrentColumn += 1;
                }

            } else if (direction == 2) {

                if (Visited == false) {
                    GameObject nextRoom = tempMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn];
                    nextRoom.SetActive(true);
                    nextRoom.GetComponent<Room>().makeAllDoorsUnvisited();
                    spawnpoint = nextRoom.GetComponent<Room>().TopSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoom.SetActive(false);
                    roomSpawner.CurrentRow += 1;
                }

            } else if (direction == 3) {
                
                if (Visited == false) {
                    GameObject nextRoom = tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1];
                    nextRoom.SetActive(true);
                    nextRoom.GetComponent<Room>().makeAllDoorsUnvisited();
                    spawnpoint = nextRoom.GetComponent<Room>().RightSpawnpoint;
                    playerTransform.position = spawnpoint.transform.position;
                    currentRoom.SetActive(false);
                    roomSpawner.CurrentColumn -= 1;
                }
            }
            Visited = true;
        }
    }
}
