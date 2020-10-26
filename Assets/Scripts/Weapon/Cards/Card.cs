using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : InteractableBase {


    #region Properties

    public EncyclopediaObject EncyclopediaObject => gameObject.GetComponent<EncyclopediaObject>();
    public Sprite SpriteUI => spriteUI;
    public abstract Dictionary<Sprite, string> Modules { get; }

    public CardType Type {
        get {
            if (this is CardGunGen)
                return CardType.CardGunGen;
            if (this is CardGunFly)
                return CardType.CardGunFly;
            if (this is CardMeleeShape)
                return CardType.CardMeleeShape;
            if (this is CardMeleeMemory)
                return CardType.CardMeleeMemory;
            if (this is CardEffect)
                return CardType.CardEffect;
            throw new System.Exception("Probably, you forget to add a new CardType to the Card script");
        }
    }
    public int CardTypeForYaricSoHeCanCalmDownAndMakeSomeUIWithoutAnyAssAcheOrSoHeCanKeepEachChairHeSitsOnByPreventingItFromFUCKINGfire => (int)Type;

    #endregion

    [SerializeField] protected Sprite spriteUI; // todo automatize

    public override bool Interact(GameObject whoInterracted) {
        var cb = whoInterracted?.GetComponent<CharacterBase>();
        //if (cb != null && cb.transform != Character) {
        //    return cb.Inventory.PickUp(this);
        //} // todo
        return false;
    }

    public enum CardType {
        CardGunGen = 1,
        CardGunFly = 2,
        CardMeleeShape = 1,
        CardMeleeMemory = 2,
        CardEffect = 3
    }
}
