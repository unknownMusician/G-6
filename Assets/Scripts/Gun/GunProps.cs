using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProps {
    public float FireRate { get; set; }
    public int ShotBltAmt { get; set; }
    public float AtkArea { get; set; }

    public GunProps(
        float fireRate = 1,
        int shotBltAmt = 1, 
        float atkArea = 1) {

        this.FireRate = fireRate;
        this.ShotBltAmt = shotBltAmt;
        this.AtkArea = atkArea;
    }
}
