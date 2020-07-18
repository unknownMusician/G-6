using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : Card {

    const string TAG = "CardGunEffect: ";

    #region Parameters

    public NestedProps Props => new NestedProps(effect, duration, interval, damage);
    public override CardType Type => CardType.CardEffect;

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    private EffectType effect = EffectType.Standard;
    [SerializeField]
    private float duration = 10f;
    [SerializeField]
    private float interval = 1f;
    [SerializeField]
    private float damage = 5f;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardGunFly (" + effect + "; " + duration + "; " + interval + "; " + damage + ")";
    }

    #endregion

    #region Inner Structures

    public class NestedProps {

        #region Parameters

        public EffectType Effect { get; }

        public float Duration { get; }

        public float Interval { get; }

        public float DMG { get; }

        #endregion

        #region Constructors

        public NestedProps(EffectType effect = EffectType.Standard, float dmg = 5f, float duration = 10f, float interval = 1f) {
            this.Effect = effect;
            this.DMG = dmg;
            this.Duration = duration;
            this.Interval = interval;
        }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunFly (" + Effect + "; " + Duration + "; " + Interval + "; " + DMG + ")";
        }

        #endregion
    }

    public enum EffectType {
        Standard,
        Frost,
        Fire,
        Poison,
        Stunn,
        Vampire
    }

    #endregion
}
