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

    void Start() {
        
    }

    public void OnClick(int blockID) {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero, new Vector3(roomSize.x * blockSize, 0, 0));
        Gizmos.DrawLine(new Vector3(roomSize.x * blockSize, 0, 0), new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0));
        Gizmos.DrawLine(new Vector3(roomSize.x * blockSize, roomSize.y * blockSize, 0), new Vector3(0, roomSize.y * blockSize, 0));
    }
}
