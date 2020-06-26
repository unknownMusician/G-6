using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunFly : CardGun {
    private BulletProps props;
    public BulletProps Props {
        get {
            if (props == null) {
                props = new BulletProps(ricochet, piercing, homing, teleporting, magnet, enemy, magnetting);
            }
            return props;
        }
    }

    [SerializeField]
    private bool ricochet = false;
    [SerializeField]
    private bool piercing = false;
    [SerializeField]
    private bool homing = false;
    [SerializeField]
    private bool teleporting = false;
    [SerializeField]
    private bool magnet = false;
    [SerializeField]
    private LayerMask enemy = 0;
    [SerializeField]
    private LayerMask magnetting = 0;

    public void Start() {
        props = new BulletProps(ricochet, piercing, homing, teleporting, magnet, enemy.value, magnetting.value);
    }
}
