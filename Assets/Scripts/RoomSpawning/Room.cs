using UnityEngine;

public class Room : MonoBehaviour {

    // roomType shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    private byte roomType;

    public static class RoomType {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }

    private void Start() {
        roomType = RoomType.regular;
    }

    public bool IsThereAnyEnemy() {
        Transform transform = GetComponent<Transform>();
        return transform.GetChild(0).childCount != 0;
    }

    public void setRoomType(byte typeOfTheRoom) {
        this.roomType = typeOfTheRoom;
    }
}
