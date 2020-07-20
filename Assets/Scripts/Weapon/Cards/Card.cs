using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : EncyclopediaObject {


    #region Properties

    public Sprite SpriteUI => spriteUI;
    public abstract Dictionary<Sprite, string> Modules { get; }

    public abstract CardType Type { get; }

    #endregion

    #region Variables

    [SerializeField]
    protected Sprite spriteUI;

    #endregion

    public enum CardType {
        CardEffect,
        CardGunGen,
        CardGunFly,
        CardMeleeShape,
        CardMeleeMemory
    }
}
