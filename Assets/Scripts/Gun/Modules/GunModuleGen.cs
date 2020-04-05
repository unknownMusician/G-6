using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleGen : GunModule {
    protected GunProps props;

    [SerializeField]
    protected float fireRateMult = 1;
    [SerializeField]
    protected int shotBltAmtAdd = 0;
    [SerializeField]
    protected float atkAreaMult = 1;

    protected void Start() {
        props = new GunProps(fireRateMult, shotBltAmtAdd, atkAreaMult);
    }

    public GunProps GetProps() {
        return props;
    }
}
