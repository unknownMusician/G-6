using System.Collections.Generic;
using UnityEngine;

namespace G6.Weapons.Cards {
    public class CardMeleeShape : Card {

        const string TAG = "CardMeleeShape: ";

        #region Properties

        public NestedProps Props { get { return new NestedProps(attackSpeedMultiplier, attackRangeMultiplier, attackDamageMultiplier); } }
        public override Dictionary<Sprite, string> Modules {
            get {
                var dict = new Dictionary<Sprite, string>();
                // To-Do: localization
                if (Props.AttackSpeedMultiplier != 1f)
                    dict.Add(moduleSprites[0], "+" + ((Props.AttackSpeedMultiplier - 1f) * 100) + "% к скорости удара.");
                if (Props.AttackRangeMultiplier != 0)
                    dict.Add(moduleSprites[1], "+" + ((Props.AttackRangeMultiplier - 1f) * 100) + "% к дальности удара.");
                if (Props.AttackDamageMultiplier != 1f)
                    dict.Add(moduleSprites[2], "+" + ((Props.AttackDamageMultiplier - 1f) * 100) + "% к урону от удара.");
                return dict;
            }
        }

        #endregion

        #region Public Variables

        [SerializeField]
        protected float attackSpeedMultiplier = 1f;
        [SerializeField]
        protected float attackRangeMultiplier = 1f;
        [SerializeField]
        protected float attackDamageMultiplier = 1f;

        //////

        [Space]
        [SerializeField]
        protected List<Sprite> moduleSprites;

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardMeleeShape (" + attackSpeedMultiplier + "; " + attackRangeMultiplier + "; " + attackDamageMultiplier + ")";
        }

        #endregion

        public class NestedProps {

            #region Parameters

            public float AttackSpeedMultiplier { get; }
            public float AttackRangeMultiplier { get; }
            public float AttackDamageMultiplier { get; }

            #endregion

            #region Constructors

            public NestedProps(
                float attackSpeedMultiplier = 1,
                float attackRangeMultiplier = 1,
                float attackDamageMultiplier = 1
                ) {
                this.AttackSpeedMultiplier = attackSpeedMultiplier;
                this.AttackRangeMultiplier = attackRangeMultiplier;
                this.AttackDamageMultiplier = attackDamageMultiplier;
            }

            #endregion

            #region Service Methods

            public override string ToString() {
                return "CardGunGenProps (" + AttackSpeedMultiplier + "; " + AttackRangeMultiplier + "; " + AttackDamageMultiplier + ")";
            }

            #endregion
        }

        #region Serialization

        [System.Serializable]
        public class Serialization {

            public float attackSpeedMultiplier;
            public float attackRangeMultiplier;
            public float attackDamageMultiplier;

            private Serialization(CardMeleeShape card) {
                attackSpeedMultiplier = card.attackSpeedMultiplier;
                attackRangeMultiplier = card.attackRangeMultiplier;
                attackDamageMultiplier = card.attackDamageMultiplier;
            }

            public static Serialization Real2Serializable(CardMeleeShape card) { return new Serialization(card); }

            public static void Serializable2Real(Serialization serialization, CardMeleeShape card) {
                card.attackSpeedMultiplier = serialization.attackSpeedMultiplier;
                card.attackRangeMultiplier = serialization.attackRangeMultiplier;
                card.attackDamageMultiplier = serialization.attackDamageMultiplier;
            }
        }

        #endregion
    }
}