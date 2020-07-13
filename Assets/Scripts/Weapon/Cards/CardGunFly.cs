using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunFly : EncyclopediaObject, CardGun {

    const string TAG = "CardGunFly: ";

    #region Parameters

    public GameObject Prefab { get; }

    public NestedProps Props { get { return new NestedProps(ricochet, piercing, homing, teleporting, magnet, enemy.value, magnetting.value); } }

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject prefab;

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

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardGunFly (" + ricochet + "; " + piercing + "; " + homing + "; " + teleporting + "; " + magnet + "; " + enemy + "; " + magnetting + ")";
    }

    #endregion

    public class NestedProps {

        #region Parameters

        public int Enemy { get; }
        public int Magnetting { get; }
        public bool Ricochet { get; }
        public bool Piercing { get; }
        public bool Homing { get; }
        public bool Teleporting { get; }
        public bool Magnet { get; }

        #endregion

        #region Constructors

        public NestedProps(
            bool ricochet = false,
            bool piercing = false,
            bool homing = false,
            bool teleporting = false,
            bool magnet = false,
            int enemy = 0,
            int magnetting = 0
            ) {
            this.Ricochet = ricochet;
            this.Piercing = piercing;
            this.Homing = homing;
            this.Teleporting = teleporting;
            this.Magnet = magnet;
            this.Enemy = enemy;
            this.Magnetting = magnetting;
        }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunFly (" + Ricochet + "; " + Piercing + "; " + Homing + "; " + Teleporting + "; " + Magnet + "; " + Enemy + "; " + Magnetting + ")";
        }

        #endregion
    }
}
