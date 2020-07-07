using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunGen : MonoBehaviour, CardGun {

    const string TAG = "CardGunGen: ";

    #region Parameters

    public CardGunGenProps Props { get { return new CardGunGenProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier); } }

    #endregion

    #region Public Variables

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

    public class CardGunGenProps {

        #region Parameters

        public float FireRateMultiplier { get; }
        public int BulletsPerShotAdder { get; }
        public float ShotRangeMultiplier { get; }

        #endregion

        #region Constructors

        public CardGunGenProps(
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
