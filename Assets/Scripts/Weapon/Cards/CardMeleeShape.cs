using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMeleeShape : EncyclopediaObject, CardMelee {

    const string TAG = "CardMeleeShape: ";

    #region Parameters

    public GameObject Prefab { get; }

    public CardMeleeShapeProps Props { get { return new CardMeleeShapeProps(attackSpeedMultiplier, attackRangeMultiplier, attackDamageMultiplier); } }

    #endregion

    #region Public Variables
    [SerializeField]
    protected GameObject prefab;


    [SerializeField]
    protected float attackSpeedMultiplier = 1f;
    [SerializeField]
    protected float attackRangeMultiplier = 1f;
    [SerializeField]
    protected float attackDamageMultiplier = 1f;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardMeleeShape (" + attackSpeedMultiplier + "; " + attackRangeMultiplier + "; " + attackDamageMultiplier + ")";
    }

    #endregion

    public class CardMeleeShapeProps {

        #region Parameters

        public float AttackSpeedMultiplier { get; }
        public float AttackRangeMultiplier { get; }
        public float AttackDamageMultiplier { get; }

        #endregion

        #region Constructors

        public CardMeleeShapeProps(
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
}
