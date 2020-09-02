using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private Vector2 blocksOffset = new Vector2(5, 5);
    
    [SerializeField]
    private float blockSize = 2;

    [SerializeField]
    private Vector2 roomSize = new Vector2(5, 5);

    [SerializeField]
    private List<GameObject> blocks = new List<GameObject>();
    private Vector3 RoomTopRightCorner => Vector3.zero;
    private Vector3 RoomTopLeftCorner => new Vector3(roomSize.x * blockSize, 0, 0);
    private Vector3 RoomBottomRightCorner => new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0);
    private Vector3 RoomBottomLeftCorner => new Vector3(0, roomSize.y * blockSize, 0);

    void Start() {
        
    }

    public void OnClick(int blockID) {
        
    }

    private void OnDrawGizmos() {
        
        Gizmos.color = Color.green;

        Gizmos.DrawLine(RoomTopRightCorner, RoomTopLeftCorner);
        Gizmos.DrawLine(RoomTopLeftCorner, RoomBottomRightCorner);
        Gizmos.DrawLine(RoomBottomRightCorner, RoomBottomLeftCorner);
        Gizmos.DrawLine(RoomBottomLeftCorner, RoomTopLeftCorner);
    
    }

    private void FillBlocksMenu() {

    }
}
