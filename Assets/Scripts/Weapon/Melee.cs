using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Melee : Weapon {

    const string TAG = "Melee: ";

    #region Properties

    public override List<GameObject> CardPrefabs {
        get {
            List<GameObject> cards = new List<GameObject>();
            if (CardShape)
                cards.Add(CardShape.Prefab);
            if (CardMemory)
                cards.Add(CardMemory.Prefab);
            if (CardEff)
                cards.Add(CardEff.Prefab);
            return cards;
        }
    }

    //////////

    protected override bool CanAttack {
        get => canAttack;
        set { if (!(canAttack = value)) SetReliefTimer(1 / ActualCardShapeProps.AttackSpeedMultiplier); }
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
    protected CardMeleeShape CardShape;
    [Space]
    [SerializeField]
    protected CardMeleeMemory CardMemory;
    [Space]
    [SerializeField]
    protected CardEffect CardEff;

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
        GetCardsFromChildren();
        InstallModCards();
    }
    public override void Attack() {
        if (state == State.Alt) {
            Shield();
        } else {
            Hit();
        }
        OnAttackAction?.Invoke();
    }
    protected override void InstallModCards() {
        InstallCard(CardShape);
        InstallCard(CardMemory);
        InstallCard(CardEff);
    }
    protected override void GetCardsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<Card>());
        }
    }

    #endregion

    #region WorkingWithCards Methods

    public bool InstallUnknownCard(Card card) => InstallCard(card as CardMeleeShape) || InstallCard(card as CardMeleeMemory) || InstallCard(card as CardEffect);

    public bool InstallCard(CardMeleeShape cardShape) {
        if (cardShape != null) {
            RemoveCard(this.CardShape);
            PrepareCardforInstall(cardShape);
            this.CardShape = cardShape;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardMeleeMemory cardMemory) {
        if (cardMemory != null) {
            RemoveCard(this.CardMemory);
            PrepareCardforInstall(cardMemory);
            this.CardMemory = cardMemory;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardEffect cardEff) {
        if (cardEff != null) {
            RemoveCard(this.CardEff);
            PrepareCardforInstall(cardEff);
            this.CardEff = cardEff;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    private void PrepareCardforInstall(Card cardGen) {
        //To-Do
    }
    private bool RemoveCard(Card card) {
        //To-Do
        return true;
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
