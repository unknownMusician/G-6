using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleGen : GunModule {
    protected GunProps props;

    [SerializeField]
    protected float fireRateMultiplier = 1;
    [SerializeField]
    protected int bulletsPerShotAdder = 0;
    [SerializeField]
    protected float bulletMassMultiplier = 1;
    [SerializeField]
    protected float shotRangeMultiplier = 1;

    protected void Start() {
        props = new GunProps(fireRateMultiplier, bulletsPerShotAdder, bulletMassMultiplier, shotRangeMultiplier);
    }

    public GunProps GetProps() {
        if(props == null) {
            props = new GunProps(fireRateMultiplier, bulletsPerShotAdder, bulletMassMultiplier, shotRangeMultiplier);
        }
        return props;
    }
}
