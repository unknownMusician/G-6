using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleGen : GunModule {
    private GunProps props;

    [SerializeField]
    private float fireRateMult = 1;
    [SerializeField]
    private int shotBltAmtAdd = 0;
    [SerializeField]
    private float atkAreaMult = 1;

    private void Start() {
        props = new GunProps(fireRateMult, shotBltAmtAdd, atkAreaMult);
    }

    public GunProps GetProps() {
        return props;
    }
}
