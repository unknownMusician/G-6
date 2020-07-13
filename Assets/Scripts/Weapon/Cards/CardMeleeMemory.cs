using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMeleeMemory : Card {

    const string TAG = "CardGunFly: ";

    #region Parameters

    public GameObject Prefab { get; }

    public NestedProps Props { get { return new NestedProps(memory); } }

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    private NestedProps.MemoryType memory = NestedProps.MemoryType.Null;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardMeleeMemory (" + memory + ")";
    }

    #endregion

    public class NestedProps {

        #region Parameters

        public MemoryType Memory { get; }

        #endregion

        #region Enums

        public enum MemoryType {
            Null,
            KungFu,
            Taekwondo,
            Karate,
            Aikido,
            DziuDzitsu,
            Dzudo
        }

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
}
