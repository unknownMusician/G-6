using UnityEngine;

public class Room : MonoBehaviour
{
    // roomtype shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    private byte roomType;

    public static class RoomType {
        public readonly static byte start = 0;
        public readonly static byte regular = 1;
        public readonly static byte finish = 2;
    }

    private void Start() {
        roomType = RoomType.regular;
    }

    public bool IsThereAnyEnemy(GameObject room) {
        return room.transform.GetChild(0).transform.childCount != 0;
    }
}
