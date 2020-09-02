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

    public void OnClick(int blockID) {
        
    }
}
