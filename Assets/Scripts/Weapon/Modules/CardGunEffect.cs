using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunEffect : CardGun {

    public CardGunEffectProps Props { get; }

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

    public CardGunEffect() {
        Props = new CardGunEffectProps(frost, fire, poison, stunn, vampire);
    }

    public CardGunEffect(CardGunEffectProps props) {
        this.Props = props;
    }

    public override string ToString() {
        return "CardGunFly (" + frost + "; " + fire + "; " + poison + "; " + stunn + "; " + vampire + ")";
    }

    public class CardGunEffectProps {
        public bool Frost { get; }
        public bool Fire { get; }
        public bool Poison { get; }
        public bool Stunn { get; }
        public bool Vampire { get; }

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

        public override string ToString() {
            return "CardGunFly (" + Frost + "; " + Fire + "; " + Poison + "; " + Stunn + "; " + Vampire + ")";
        }
    }
}
