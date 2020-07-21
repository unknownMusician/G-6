using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : EncyclopediaObject {


    #region Properties

    public Sprite SpriteUI => spriteUI;
    public abstract Dictionary<Sprite, string> Modules { get; }

    public abstract CardType Type { get; }
    public int CardTypeForYaricSoHeCanCalmDownAndMakeSomeUIWithoutAnyAssAcheOrSoHeCanKeepEachChairHeSitsOnByPreventingItFromFUCKINGfire => cardTypeForYaric;

    #endregion

    #region Variables

    [SerializeField]
    protected Sprite spriteUI;
    [SerializeField]
    protected int cardTypeForYaric;

    #endregion

    public enum CardType {
        CardEffect,
        CardGunGen,
        CardGunFly,
        CardMeleeShape,
        CardMeleeMemory
    }
}
