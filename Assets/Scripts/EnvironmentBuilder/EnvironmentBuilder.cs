using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder : MonoBehaviour {

    [SerializeField]
    private Vector2 blocksOffset = new Vector2(5, 5);
    
    [SerializeField]
    private float blockSize = 0;

    [SerializeField]
    private Vector2 roomSize = new Vector2(5, 5);

    [SerializeField]
    private List<GameObject> blocks = new List<GameObject>();

    public void OnClick(int blockID) {
        
    }

    private void OnDrawGizmos() {
        
    }

    private void FillBlocksMenu() {

    }
}
