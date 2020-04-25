using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : CharacterBase 
{

    public Transform player;
    new protected void Start() 
    {

        MaxHP = 100f;
        MaxSP = 100f;
        MaxOP = 100f;

        HP = MaxHP;
        SP = MaxSP;
        OP = MaxOP;

        base.Start();
    }
    new protected void Update() 
    {
        base.Update();

        float dist = (player.position - this.transform.position).magnitude;
        if (dist > 50) 
            this.transform.position = player.position + Vector3.forward * 5;
    }
    new protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void WeaponControl()
    {
        
    }

    protected override void WeaponFixedControl()
    {
        
    }
}
