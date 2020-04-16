using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProps {
    public float FireRateMultiplier { get; set; }
    public int BulletsPerShotAdder { get; set; }
    public float BulletMassMultiplier { get; set; }
    public float ShotRangeMultiplier { get; set; }

    public GunProps(
        float fireRateMultiplier = 1,
        int bulletsPerShotAdder = 0, 
        float bulletMassMultiplier = 1,
        float shotRangeMultiplier = 1) {

        this.FireRateMultiplier = fireRateMultiplier;
        this.BulletsPerShotAdder = bulletsPerShotAdder;
        this.BulletMassMultiplier = bulletMassMultiplier;
        this.ShotRangeMultiplier = shotRangeMultiplier;
    }
}
