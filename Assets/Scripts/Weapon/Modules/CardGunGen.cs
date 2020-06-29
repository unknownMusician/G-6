using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunGen : CardGun {

    public CardGunGenProps Props { get; }

    [SerializeField]
    protected float fireRateMultiplier = 1;
    [SerializeField]
    protected int bulletsPerShotAdder = 0;
    [SerializeField]
    protected float shotRangeMultiplier = 1;

    public CardGunGen() {
        Props = new CardGunGenProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier);
    }

    public CardGunGen(CardGunGenProps props) {
        this.Props = props;
    }

    public override string ToString() {
        return "CardGunGen (" + fireRateMultiplier + "; " + bulletsPerShotAdder + "; " + shotRangeMultiplier + ")";
    }

    public class CardGunGenProps {
        public float FireRateMultiplier { get; }
        public int BulletsPerShotAdder { get; }
        public float ShotRangeMultiplier { get; }

        public CardGunGenProps(
            float fireRateMultiplier = 1,
            int bulletsPerShotAdder = 0,
            float shotRangeMultiplier = 1
            ) {
            this.FireRateMultiplier = fireRateMultiplier;
            this.BulletsPerShotAdder = bulletsPerShotAdder;
            this.ShotRangeMultiplier = shotRangeMultiplier;
        }

        public override string ToString() {
            return "CardGunGenProps (" + FireRateMultiplier + "; " + BulletsPerShotAdder + "; " + ShotRangeMultiplier + ")";
        }
    }
}
