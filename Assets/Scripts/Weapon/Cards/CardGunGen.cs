using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunGen : Card {

    const string TAG = "CardGunGen: ";

    #region Properties

    public NestedProps Props => new NestedProps(fireRateMultiplier, bulletsPerShotAdder, shotRangeMultiplier);
    public override Dictionary<Sprite, string> Modules {
        get {
            var dict = new Dictionary<Sprite, string>();
            // To-Do: localization
            if (Props.FireRateMultiplier != 1f)
                dict.Add(moduleSprites[0], "+" + ((Props.FireRateMultiplier - 1f) * 100) + "% к скорострельности.");
            if (Props.BulletsPerShotAdder != 0)
                dict.Add(moduleSprites[1], "+" + Props.BulletsPerShotAdder + " пуль к выстрелу.");
            if (Props.ShotRangeMultiplier != 1f)
                dict.Add(moduleSprites[2], "+" + ((Props.ShotRangeMultiplier - 1f) * 100) + "% к дальности выстрела.");
            return dict;
        }
    }

    #endregion

    #region Public Variables

    [SerializeField]
    protected float fireRateMultiplier = 1f;
    [SerializeField]
    protected int bulletsPerShotAdder = 0;
    [SerializeField]
    protected float shotRangeMultiplier = 1f;

    //////

    [Space]
    [SerializeField]
    protected List<Sprite> moduleSprites; // todo automatize

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

    #region Serialization

    [System.Serializable]
    public class Serialization {

        public float fireRateMultiplier;
        public int bulletsPerShotAdder;
        public float shotRangeMultiplier;

        private Serialization(CardGunGen card) {
            fireRateMultiplier = card.fireRateMultiplier;
            bulletsPerShotAdder = card.bulletsPerShotAdder;
            shotRangeMultiplier = card.shotRangeMultiplier;
        }

        public static Serialization Real2Serializable(CardGunGen card) { return new Serialization(card); }

        public static void Serializable2Real(Serialization serialization, CardGunGen card) {
            card.fireRateMultiplier = serialization.fireRateMultiplier;
            card.bulletsPerShotAdder = serialization.bulletsPerShotAdder;
            card.shotRangeMultiplier = serialization.shotRangeMultiplier;
        }
    }

    #endregion
}
