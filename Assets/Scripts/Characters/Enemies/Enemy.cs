using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : CharacterBase 
{

    public Transform player;
    new protected void Update() 
    {
        base.Update();

        float dist = (player.position - this.transform.position).magnitude;
        if (dist > 50) 
            this.transform.position = player.position + Vector3.forward * 5;
    }
}
