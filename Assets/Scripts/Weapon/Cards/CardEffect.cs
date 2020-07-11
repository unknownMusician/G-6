using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : EncyclopediaObject, CardGun, CardMelee {

    const string TAG = "CardGunEffect: ";

    #region Parameters

    public GameObject Prefab { get; }

    public CardGunEffectProps Props { get { return new CardGunEffectProps(frost, fire, poison, stunn, vampire); } }

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    private bool frost = false;
    [SerializeField]
    private bool fire = false;
    [SerializeField]
    private bool poison = false;
    [SerializeField]
    private bool stunn = false;
    [SerializeField]
    private bool vampire = false;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardGunFly (" + frost + "; " + fire + "; " + poison + "; " + stunn + "; " + vampire + ")";
    }

    #endregion

    public class CardGunEffectProps {

        #region Parameters

        public bool Frost { get; }
        public bool Fire { get; }
        public bool Poison { get; }
        public bool Stunn { get; }
        public bool Vampire { get; }

        #endregion

        #region Constructors

        public CardGunEffectProps(
            bool frost = false,
            bool fire = false,
            bool poison = false,
            bool stunn = false,
            bool vampire = false
            ) {
            Frost = frost;
            Fire = fire;
            Poison = poison;
            Stunn = stunn;
            Vampire = vampire;
        }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunFly (" + Frost + "; " + Fire + "; " + Poison + "; " + Stunn + "; " + Vampire + ")";
        }

        #endregion
    }
}
