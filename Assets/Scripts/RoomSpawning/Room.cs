using UnityEngine;

public class Room : MonoBehaviour
{
    // roomtype shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    public byte roomType;
    
    // GameObject, which contains enemies in the room
    public GameObject enemies;

    public Room(GameObject roomGameObject) {
        enemies = roomGameObject.transform.GetChild(0).gameObject;
        roomType = RoomType.regular;
    }

    public bool IsThereAnyEnemy(GameObject room) {
        return room.transform.GetChild(0).transform.childCount != 0;
    }

    public static class RoomType {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }
}
