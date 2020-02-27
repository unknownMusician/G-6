using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (player.position - this.transform.position).magnitude;
        if(dist> 30)
        {
            this.transform.position = player.position + Vector3.forward * 5;
        }
    }
}
