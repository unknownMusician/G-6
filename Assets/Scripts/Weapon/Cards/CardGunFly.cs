using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGunFly : Card {

    const string TAG = "CardGunFly: ";

    #region Properties

    public NestedProps Props => new NestedProps(ricochet, piercing, homing, teleporting, magnet, enemy.value, magnetting.value);
    public override Dictionary<Sprite, string> Modules {
        get {
            var dict = new Dictionary<Sprite, string>();
            // To-Do: localization
            if (Props.Ricochet)
                dict.Add(moduleSprites[0], "Рикошет.");
            if (Props.Piercing)
                dict.Add(moduleSprites[1], "Бронебойность.");
            if (Props.Homing)
                dict.Add(moduleSprites[2], "Самонаведение на " + Props.Enemy + ".");
            if (Props.Teleporting)
                dict.Add(moduleSprites[3], "Телепортация.");
            if (Props.Magnet)
                dict.Add(moduleSprites[4], "Притягивание " + Props.Magnetting + ".");
            return dict;
        }
    }

    #endregion

    #region Public Variables

    [SerializeField]
    private bool ricochet = false;
    [SerializeField]
    private bool piercing = false;
    [SerializeField]
    private bool homing = false;
    [SerializeField]
    private bool teleporting = false;
    [SerializeField]
    private bool magnet = false;
    [SerializeField]
    private LayerMask enemy = 0;
    [SerializeField]
    private LayerMask magnetting = 0;

    //////

    [Space]
    [SerializeField]
    protected List<Sprite> moduleSprites;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardGunFly (" + ricochet + "; " + piercing + "; " + homing + "; " + teleporting + "; " + magnet + "; " + enemy + "; " + magnetting + ")";
    }

    #endregion

    public class NestedProps {

        #region Parameters

        public LayerMask Enemy { get; } = 0;
        public LayerMask Magnetting { get; } = 0;
        public bool Ricochet { get; } = false;
        public bool Piercing { get; } = false;
        public bool Homing { get; } = false;
        public bool Teleporting { get; } = false;
        public bool Magnet { get; } = false;

        #endregion

        #region Constructors

        public NestedProps(
            bool ricochet,
            bool piercing,
            bool homing,
            bool teleporting,
            bool magnet,
            LayerMask enemy,
            LayerMask magnetting
            ) {
            this.Ricochet = ricochet;
            this.Piercing = piercing;
            this.Homing = homing;
            this.Teleporting = teleporting;
            this.Magnet = magnet;
            this.Enemy = enemy;
            this.Magnetting = magnetting;
        }
        public NestedProps(bool ricochet, bool piercing, bool teleporting) {
            this.Ricochet = ricochet;
            this.Piercing = piercing;
            this.Teleporting = teleporting;
        }
        public NestedProps() { }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunFly (" + Ricochet + "; " + Piercing + "; " + Homing + "; " + Teleporting + "; " + Magnet + "; " + Enemy + "; " + Magnetting + ")";
        }

        #endregion
    }

    #region Serialization

    [System.Serializable]
    public class Serialization {

        public int enemy;
        public int magnetting;
        public bool ricochet;
        public bool piercing;
        public bool homing;
        public bool teleporting;
        public bool magnet;

        private Serialization(CardGunFly card) {
            enemy = card.enemy.value;
            magnetting = card.magnetting.value;
            ricochet = card.ricochet;
            piercing = card.piercing;
            homing = card.homing;
            teleporting = card.teleporting;
            magnet = card.magnet;
        }

        public static Serialization Real2Serializable(CardGunFly card) { return new Serialization(card); }

        public static void Serializable2Real(Serialization serialization, CardGunFly card) {
            card.enemy =  1 << serialization.enemy; // todo: check layermask
            card.magnetting = 1 << serialization.magnetting; // todo: check layermask
            card.ricochet = serialization.ricochet;
            card.piercing = serialization.piercing;
            card.homing = serialization.homing;
            card.teleporting = serialization.teleporting;
            card.magnet = serialization.magnet;
        }
    }

    #endregion
}
