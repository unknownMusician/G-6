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

    // GameObject, which contains content of the whole room
    public GameObject room;

    public Room(GameObject roomGameObject) {
        room = roomGameObject;
        enemies = roomGameObject.transform.GetChild(0).gameObject;
        roomType = 1;
    } 

    public bool IsThereAnyEnemy(GameObject room) {
        if (room.transform.GetChild(0).transform.childCount != 0) {
            return true;
        }
        return false;
    }
}
