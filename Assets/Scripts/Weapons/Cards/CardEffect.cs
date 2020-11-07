using System.Collections.Generic;
using UnityEngine;

namespace G6.Weapons.Cards {
    public class CardEffect : Card {

        const string TAG = "CardGunEffect: ";

        #region Properties

        public NestedProps Props => new NestedProps(effect, duration, interval, damage);
        public override Dictionary<Sprite, string> Modules {
            get {
                var dict = new Dictionary<Sprite, string>();
                // To-Do: localization
                if (Props.Effect != EffectType.Standard)
                    dict.Add(moduleSprites[(int)Props.Effect], "Заклинает врага на " + Props.Effect + " на " + Props.Duration + "сек, нанося " + Props.DMG + "урона каждые " + Props.Interval + "сек" + ".");
                return dict;
            }
        }

        #endregion

        #region Public Variables

        [SerializeField]
        private EffectType effect = EffectType.Standard;
        [SerializeField]
        private float duration = 10f;
        [SerializeField]
        private float interval = 1f;
        [SerializeField]
        private float damage = 5f;

        //////

        [Space]
        [SerializeField]
        protected List<Sprite> moduleSprites;

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
            Standard = -1,
            Frost = 0,
            Fire = 1,
            Poison = 2,
            Stunn = 3,
            Vampire = 4
        }

        #endregion

        #region Serialization

        [System.Serializable]
        public class Serialization {

            public int effect;
            public float duration;
            public float interval;
            public float damage;

            private Serialization(CardEffect card) {
                effect = (int)card.effect;
                duration = card.duration;
                interval = card.interval;
                damage = card.damage;
            }

            public static Serialization Real2Serializable(CardEffect card) { return new Serialization(card); }

            public static void Serializable2Real(Serialization serialization, CardEffect card) {
                card.effect = (EffectType)serialization.effect;
                card.duration = serialization.duration;
                card.interval = serialization.interval;
                card.damage = serialization.damage;
            }
        }

        #endregion
    }
}
