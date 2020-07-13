using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunGen : EncyclopediaObject, CardGun {

    const string TAG = "CardGunGen: ";

    #region Parameters

    public GameObject Prefab { get; }

    public NestedProps Props { get { return new NestedProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier); } }

    #endregion

    #region Public Variables
    [SerializeField]
    protected GameObject prefab;


    [SerializeField]
    protected float fireRateMultiplier = 1;
    [SerializeField]
    protected int bulletsPerShotAdder = 0;
    [SerializeField]
    protected float shotRangeMultiplier = 1;

    #endregion

    #region Service Methods
 
    public override string ToString() {
        return "CardGunGen (" + fireRateMultiplier + "; " + bulletsPerShotAdder + "; " + shotRangeMultiplier + ")";
    }

    #endregion

    public class NestedProps {

        #region Parameters

        public float FireRateMultiplier { get; }
        public int BulletsPerShotAdder { get; }
        public float ShotRangeMultiplier { get; }

        #endregion

        #region Constructors

        public NestedProps(
            float fireRateMultiplier = 1,
            int bulletsPerShotAdder = 0,
            float shotRangeMultiplier = 1
            ) {
            this.FireRateMultiplier = fireRateMultiplier;
            this.BulletsPerShotAdder = bulletsPerShotAdder;
            this.ShotRangeMultiplier = shotRangeMultiplier;
        }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunGenProps (" + FireRateMultiplier + "; " + BulletsPerShotAdder + "; " + ShotRangeMultiplier + ")";
        }

        #endregion
    }
}
