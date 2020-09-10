using UnityEngine;

public class Door : MonoBehaviour {

    #region Variables

    private RoomSpawner roomSpawner;

    private GameObject[,] roomMatrix;
    private GameObject[,] miniMapMatrix;

    public bool Visited { get; set; }

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
        if (this.name[0] == 'T') {
            direction = 0;
        } else if (this.name[0] == 'R') {
            direction = 1;
        } else if (this.name[0] == 'B') {
            direction = 2;
        } else if (this.name[0] == 'L') {
            direction = 3;
        }
    }

    void Start() {

        roomSpawner = MainData.RoomSpawnerObject?.GetComponent<RoomSpawner>();
        miniMapMatrix = roomSpawner?.getMiniMapMatrix();
        roomMatrix = roomSpawner?.getRoomsMatrix();
        Visited = false;
    
    }

    #endregion

    void OnTriggerEnter2D(Collider2D other) {
        if (roomSpawner != null) {
            if ((other.name == "Player") & (Visited == false)) {
                roomSpawner.goToNextRoom(other.transform, direction);
                Visited = true;
            }
        } else {
            // todo: it is for when you use EnvironmentBuilder;
        }
    }

    #endregion

}
