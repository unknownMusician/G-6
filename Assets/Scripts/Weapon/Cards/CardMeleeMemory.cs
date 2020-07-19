using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMeleeMemory : Card {

    const string TAG = "CardGunFly: ";

    #region Properties

    public NestedProps Props { get { return new NestedProps(memory); } }
    public override CardType Type => CardType.CardMeleeMemory;
    public override Dictionary<Sprite, string> Modules {
        get {
            var dict = new Dictionary<Sprite, string>();
            // To-Do: localization
            if (Props.Memory != MemoryType.Null)
                dict.Add(moduleSprites[(int)Props.Memory], "Стиль боя: " + Props.Memory + ".");
            return dict;
        }
    }

    #endregion

    #region Public Variables

    [SerializeField]
    private MemoryType memory = MemoryType.Null;

    //////

    [Space]
    [SerializeField]
    protected List<Sprite> moduleSprites;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardMeleeMemory (" + memory + ")";
    }

    #endregion

    #region Inner Structures

    public class NestedProps {

        #region Parameters

        public MemoryType Memory { get; }

        #endregion

        #region Constructors

        public NestedProps(MemoryType memory = MemoryType.Null) {
            this.Memory = memory;
        }

        #endregion

        #region Service Methods

        public override string ToString() {
            return "CardGunFly (" + Memory + ")";
        }

        #endregion
    }

    public enum MemoryType {
        Null = -1,
        KungFu = 0,
        Taekwondo = 1,
        Karate = 2,
        Aikido = 3,
        DziuDzitsu = 4,
        Dzudo = 5
    }

    #endregion
}
