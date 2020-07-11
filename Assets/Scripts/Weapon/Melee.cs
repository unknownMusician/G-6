using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon {

    const string TAG = "Melee: ";

    #region Properties

    public override List<GameObject> AllCardPrefabList {
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
        get { return canAttack; }
        set { canAttack = value; if (!value) SetReliefTimer(1 / ActualCardShapeProps.AttackSpeedMultiplier); }
    }
    private Vector3 WorldHitCentrePoint { get { return transform.position + this.transform.rotation * localHitCentrePoint; } }

    private CardMeleeShape.CardMeleeShapeProps ActualCardShapeProps { get { return CardShape == null ? StandardCardShapeProps : CardShape.Props; } }
    private CardMeleeMemory.CardMeleeMemoryProps ActualCardMemoryProps { get { return CardMemory == null ? StandardCardMemoryProps : CardMemory.Props; } }
    private CardEffect.CardGunEffectProps ActualCardEffectProps { get { return CardEff == null ? StandardCardEffProps : CardEff.Props; } }

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

    protected CardMeleeShape.CardMeleeShapeProps StandardCardShapeProps = new CardMeleeShape.CardMeleeShapeProps();
    protected CardMeleeMemory.CardMeleeMemoryProps StandardCardMemoryProps = new CardMeleeMemory.CardMeleeMemoryProps();
    protected CardEffect.CardGunEffectProps StandardCardEffProps = new CardEffect.CardGunEffectProps();

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
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<CardGun>());
        }
    }

    #endregion

    #region WorkingWithCards Methods

    public bool InstallUnknownCard(CardGun card) {
        if (card is CardMeleeShape) {
            return InstallCard((CardMeleeShape)card);
        } else if (card is CardMeleeMemory) {
            return InstallCard((CardMeleeMemory)card);
        } else if (card is CardEffect) {
            return InstallCard((CardEffect)card);
        }
        return false;
    }
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
    private void PrepareCardforInstall(CardMelee cardGen) {
        //To-Do
    }
    private bool RemoveCard(CardMelee card) {
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
            HashSet<GameObject> objs = new HashSet<GameObject>();
            foreach (Collider2D col in cols) {
                objs.Add(col.gameObject);
            }
            int actualHitCounter = 0;
            foreach (GameObject obj in objs) {
                if (!obj.Equals(this.transform.parent.parent.gameObject)) {
                    Vector3 hitPoint = obj.transform.position;
                    CharacterBase cb = obj.GetComponent<CharacterBase>();
                    if (cb != null) {
                        cb.TakeDamage((hitPoint - this.transform.position).normalized * standardDamage);
                        actualHitCounter++;
                    }
                }
            }
            CanAttack = false;
            Debug.Log(TAG + "Hit (" + actualHitCounter + " target" + ((actualHitCounter == 1) ? "" : "s") + ")");
        }
    }

    #endregion

    #region Inner Classes

    public new class Info : Weapon.Info {

        public Info(GameObject weaponPrefab, List<GameObject> cardPrefabs)
            : base(weaponPrefab, cardPrefabs) {
            //
        }
    }

    #endregion
}
