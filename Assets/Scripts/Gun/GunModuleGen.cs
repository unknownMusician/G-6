using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleGen : GunModule {
    private GunProps props;

    [SerializeField]
    private float fireRateMult;
    [SerializeField]
    private int shotBltAmtAdd;
    [SerializeField]
    private float atkAreaMult;

    private void Start() {
        props = new GunProps(fireRateMult, shotBltAmtAdd, atkAreaMult);
    }

    public GunProps GetProps() {
        return props;
    }
}
