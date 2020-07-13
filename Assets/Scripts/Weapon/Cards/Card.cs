using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : EncyclopediaObject {


    #region Properties

    public GameObject Prefab { get; }

    public abstract CardType Type { get; }

    #endregion

    public enum CardType {
        CardEffect,
        CardGunGen,
        CardGunFly,
        CardMeleeShape,
        CardMeleeMemory
    }
}
