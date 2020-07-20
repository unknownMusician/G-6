﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Melee : Weapon {

    const string TAG = "Melee: ";

    #region Properties

    public CardMeleeShape CardShape { get => cardShape; private set => cardShape = value; }
    public CardMeleeMemory CardMemory { get => cardMemory; private set => cardMemory = value; }
    public CardEffect CardEff { get => cardEff; private set => cardEff = value; }

    ////////////

    protected override bool CanAttack {
        get => canAttack;
        set {
            if (!(canAttack = value))
                SetReliefTimer(1 / ActualCardShapeProps.AttackSpeedMultiplier);
        }
    }
    private Vector3 WorldHitCentrePoint => transform.position + this.transform.rotation * localHitCentrePoint;

    private CardMeleeShape.NestedProps ActualCardShapeProps => CardShape?.Props ?? StandardCardShapeProps;
    private CardMeleeMemory.NestedProps ActualCardMemoryProps => CardMemory?.Props ?? StandardCardMemoryProps;
    private CardEffect.NestedProps ActualCardEffectProps => CardEff?.Props ?? StandardCardEffProps;

    #endregion

    #region Public Variables

    [Space]
    [Space]
    [SerializeField]
    private Vector3 localHitCentrePoint = Vector3.right;
    [SerializeField]
    private float hitAreaRadius = 1f;

    [Space]
    [Space]
    [SerializeField]
    private float standardDamage = 10f;

    [Space]
    [Space]
    [SerializeField]
    private CardMeleeShape cardShape;
    [Space]
    [SerializeField]
    private CardMeleeMemory cardMemory;
    [Space]
    [SerializeField]
    private CardEffect cardEff;

    #endregion

    #region Private Variables

    protected CardMeleeShape.NestedProps StandardCardShapeProps = new CardMeleeShape.NestedProps();
    protected CardMeleeMemory.NestedProps StandardCardMemoryProps = new CardMeleeMemory.NestedProps();
    protected CardEffect.NestedProps StandardCardEffProps = new CardEffect.NestedProps();

    #endregion

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(WorldHitCentrePoint, hitAreaRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector3.right);
    }

    #endregion

    #region Service Methods

    //private void SendBulletsToMainData() {
    //    MainData.ClipBullets = clipActualBullets;
    //    MainData.PocketBullets = pocketActualBullets;
    //}

    #endregion

    #region Overrided Methods

    private void Start() {
        InstallCardsFromChildren();
    }
    public override void Attack() {
        if (state == State.Alt) {
            Shield();
        } else {
            Hit();
        }
        OnAttackAction?.Invoke();
    }
    protected override void InstallCardsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<Card>());
        }
    }

    public override bool InstallUnknownCard(Card card) => InstallCard(card as CardMeleeShape) || InstallCard(card as CardMeleeMemory) || InstallCard(card as CardEffect);
    public override bool UninstallUnknownCard(Card card) {
        if (card != null) {
            bool answer = false;
            if (answer = card == CardShape)
                CardShape = null;
            else if (answer = card == CardMemory)
                CardMemory = null;
            else if (answer = card == CardEff)
                CardEff = null;
            else
                Debug.Log(TAG + "ERROR IN CARDS TYPE WHEN REMOVING CARD");
            if (answer) {
                MainData.Inventory.Cards.Add(card);
                MainData.Inventory.Cards = MainData.Inventory.Cards;
            }
            return answer;
        }
        return false;
    }

    #endregion

    #region WorkingWithCards Methods

    public bool InstallCard(CardMeleeShape cardShape) {
        if (cardShape != null) {
            UninstallUnknownCard(this.CardShape);
            PrepareCardforInstall(cardShape);
            this.CardShape = cardShape;
            MainData.Inventory.Cards.Remove(cardShape);
            MainData.Inventory.Cards = MainData.Inventory.Cards;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardMeleeMemory cardMemory) {
        if (cardMemory != null) {
            UninstallUnknownCard(this.CardMemory);
            PrepareCardforInstall(cardMemory);
            this.CardMemory = cardMemory;
            MainData.Inventory.Cards.Remove(cardMemory);
            MainData.Inventory.Cards = MainData.Inventory.Cards;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardEffect cardEff) {
        if (cardEff != null) {
            UninstallUnknownCard(this.CardEff);
            PrepareCardforInstall(cardEff);
            this.CardEff = cardEff;
            MainData.Inventory.Cards.Remove(cardEff);
            MainData.Inventory.Cards = MainData.Inventory.Cards;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    private void PrepareCardforInstall(Card cardGen) {
        //To-Do
    }

    #endregion

    #region Main Methods

    private void Shield() {
        // To-Do
    }
    private void Hit() {
        if (CanAttack) {
            Collider2D[] cols = Physics2D.OverlapCircleAll(WorldHitCentrePoint, hitAreaRadius);
            //
            int actualHits = (from col in cols
                              group col by col.gameObject into gameObj
                              where !gameObj.Key.Equals(this.transform.parent.parent.gameObject)
                              group gameObj.Key by gameObj.Key.GetComponent<CharacterBase>() into charBase
                              where charBase.Key != null
                              let hitPoint = charBase.Key.gameObject.transform.position
                              select charBase.Key)
                        .Select(x => {
                            x.TakeDamage((x.gameObject.transform.position - transform.position).normalized * standardDamage);
                            return x;
                        })
                        .Count();
            CanAttack = false;
            Debug.Log(TAG + "Hit (" + actualHits + " target" + ((actualHits == 1) ? "" : "s") + ")");
        }
    }

    #endregion
}
