using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunGen : CardGun {
    protected GunProps props;
    public GunProps Props {
        get {
            if (props == null) {
                props = new GunProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier);
            }
            return props;
        }
    }

    [SerializeField]
    protected float fireRateMultiplier = 1;
    [SerializeField]
    protected int bulletsPerShotAdder = 0;
    [SerializeField]
    protected float shotRangeMultiplier = 1;

    protected void Start() {
        props = new GunProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier);
    }
}
