using UnityEngine;

public class DoorsScript : MonoBehaviour {

    private RoomSpawner roomSpawner;
    public byte direction;
    private GameObject[,] tempMatrix;

    // directions - отвечает за направление в которое смотрит дверь
    // 1 - вверх
    // 2 - вправо
    // 3 - вниз
    // 4 - влево

    void OnTriggerEnter2D(Collider2D other) {

        if (other.name == "Player") {

            Transform playerTransform = other.transform;
            roomSpawner = this.transform.parent.parent.parent.GetComponent<RoomSpawner>();
            tempMatrix = roomSpawner.GetRoomsMatrix();

            if (direction == 1) {

                tempMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn].SetActive(true);
                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn].SetActive(false);

                Transform nextRoomIerarchy = tempMatrix[roomSpawner.CurrentRow - 1, roomSpawner.CurrentColumn].transform;
                int amountOfChilds = nextRoomIerarchy.childCount;

                for (int i = 0; i < amountOfChilds; i++) {

                    Transform tempChild = nextRoomIerarchy.GetChild(i);
                    if (tempChild.name == "BottomSpawnpoint") {

                        Transform spawnPoint = tempChild;
                        playerTransform.position = spawnPoint.position;
                        roomSpawner.CurrentRow -= 1;

                    }
                }


            } else if (direction == 2) {

                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1].SetActive(true);
                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn].SetActive(false);

                Transform roomIerarchy = tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn + 1].transform;
                int amountOfChilds = roomIerarchy.childCount;

                for (int i = 0; i < amountOfChilds; i++) {

                    Transform tempChild = roomIerarchy.GetChild(i);
                    if (tempChild.name == "LeftSpawnpoint") {

                        Transform spawnPoint = tempChild;
                        playerTransform.position = spawnPoint.position;
                        roomSpawner.CurrentColumn += 1;

                    }
                }


            } else if (direction == 3) {

                tempMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn].SetActive(true);
                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn].SetActive(false);


                Transform roomIerarchy = tempMatrix[roomSpawner.CurrentRow + 1, roomSpawner.CurrentColumn].transform;
                int amountOfChilds = roomIerarchy.childCount;

                for (int i = 0; i < amountOfChilds; i++) {

                    Transform tempChild = roomIerarchy.GetChild(i);
                    if (tempChild.name == "TopSpawnpoint") {

                        Transform spawnPoint = tempChild;
                        playerTransform.position = spawnPoint.position;
                        roomSpawner.CurrentRow += 1;

                    }
                }


            } else if (direction == 4) {

                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1].SetActive(true);
                tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn].SetActive(false);

                Transform roomIerarchy = tempMatrix[roomSpawner.CurrentRow, roomSpawner.CurrentColumn - 1].transform;
                int amountOfChilds = roomIerarchy.childCount;

                for (int i = 0; i < amountOfChilds; i++) {

                    Transform tempChild = roomIerarchy.GetChild(i);
                    if (tempChild.name == "RightSpawnpoint") {

                        Transform spawnPoint = tempChild;
                        playerTransform.position = spawnPoint.position;
                        roomSpawner.CurrentColumn -= 1;

                    }
                }
            }
        }
    }
}
