using UnityEngine;

public class Room : MonoBehaviour {
    
    // roomType shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    public byte roomType;

    // GameObject, which contains enemies in the room
    public GameObject enemies;

    ////////////////////////////////////////////////////////////
    // fixed by unknownMusician
    // check if it's correct & delete these comments

    public Room() {
        roomType = RoomType.regular;
    }

    private void Start() {
        enemies = this.gameObject.transform.GetChild(0).gameObject;
    }

    //public Room(GameObject roomGameObject) {
    //    enemies = roomGameObject.transform.GetChild(0).gameObject; // You can't do this. When Constructor called, there's no GameObject yet, 
    //    roomType = RoomType.regular; //                            // as soon as Transform, Rigidbody and other Components.
    //}

    ////////////////////////////////////////////////////////////

    public bool IsThereAnyEnemy(GameObject room) {
        return room.transform.GetChild(0).childCount != 0;
    }

    public static class RoomType {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }
}
