using UnityEngine;

public class Room : MonoBehaviour {
    
    // roomType shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    public byte roomType;

    public static class RoomType {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }

    public bool IsThereAnyEnemy(GameObject room) {
        return room.transform.GetChild(0).childCount != 0;
    }
}
