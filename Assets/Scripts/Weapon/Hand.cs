using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    
    public void Aim(Vector3 worldPoint) {
        Vector2 distance = worldPoint - this.transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(distance.y, distance.x);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
