using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMeleeMemory : MonoBehaviour, CardMelee {

    const string TAG = "CardGunFly: ";

    #region Parameters

    public GameObject Prefab { get; }

    public CardMeleeMemoryProps Props { get { return new CardMeleeMemoryProps(memory); } }

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject prefab;

    [SerializeField]
    private CardMeleeMemoryProps.MemoryType memory;

    #endregion

    #region Service Methods

    public override string ToString() {
        return "CardMeleeMemory (" + memory + ")";
    }

    #endregion

    public class CardMeleeMemoryProps {

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

        public CardMeleeMemoryProps(MemoryType memory = MemoryType.Null) {
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
