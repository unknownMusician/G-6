using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModuleFly : GunModule {
    private BulletProps props;

    [SerializeField]
    private bool ricochet;
    [SerializeField]
    private bool piercing;
    [SerializeField]
    private bool homing;
    [SerializeField]
    private bool teleporting;
    [SerializeField]
    private bool magnet;
    [SerializeField]
    private LayerMask enemy;
    [SerializeField]
    private LayerMask magnetting;

    public void Start() {
        props = new BulletProps(ricochet, piercing, homing, teleporting, magnet, enemy.value, magnetting.value);
    }

    public BulletProps GetProps() {
        return props;
    }
}
