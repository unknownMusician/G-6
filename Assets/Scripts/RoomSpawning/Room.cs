using UnityEngine;

public class Room : MonoBehaviour {

    // roomType shows type of the room
    // 0 - start room
    // 1 - regular room
    // 2 - finish room
    public byte RoomType { get; set; }
    private Doors TopDoor { get; set; }
    private Doors RightDoor { get; set; }
    private Doors BottomDoor { get; set; }
    private Doors LeftDoor { get; set; }

    public static class TypeOfTheRoom {
        readonly public static byte start = 0;
        readonly public static byte regular = 1;
        readonly public static byte finish = 2;
    }

    private void Start() {
        RoomType = TypeOfTheRoom.regular;
        Transform doorsCollectionObject = this.transform.GetChild(1);
        int amountOfDoors = doorsCollectionObject.childCount;
        for (int i = 0; i < amountOfDoors; i++) {
            GameObject door = doorsCollectionObject.GetChild(i).gameObject;
            if (door.name == "TopDoor") {
                TopDoor = door.GetComponent<Doors>();
            } else if (door.name == "RightDoor") {
                RightDoor = door.GetComponent<Doors>();
            } else if (door.name == "BottomDoor") {
                BottomDoor = door.GetComponent<Doors>();
            } else if (door.name == "LeftDoor") {
                LeftDoor = door.GetComponent<Doors>();
            }
        }
    }

    public bool IsThereAnyEnemy() {
        Transform transform = GetComponent<Transform>();
        return transform.GetChild(0).childCount != 0;
    }
}
