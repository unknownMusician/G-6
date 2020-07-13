using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : EncyclopediaObject {


    #region Properties

    public GameObject Prefab { get; }

    public abstract NestedInfo Info { get; }

    #endregion

    public class NestedInfo {
        public GameObject Prefab;
        public CardType Type;

        public NestedInfo(GameObject prefab, CardType type) {
            this.Prefab = prefab;
            this.Type = type;
        }
    }

    public enum CardType {
        CardEffect,
        CardGunGen,
        CardGunFly,
        CardMeleeShape,
        CardMeleeMemory
    }
}
