using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {


    #region Properties

    public EncyclopediaObject EncyclopediaObject => gameObject.GetComponent<EncyclopediaObject>();
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
