using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon {

    const string TAG = "Melee: ";

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
    private float standardDamage;

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

    protected CardMeleeShape.CardMeleeShapeProps StandardCardShapeProps;
    protected CardMeleeMemory.CardMeleeMemoryProps StandardCardMemoryProps;
    protected CardEffect.CardGunEffectProps StandardCardEffProps;

    #endregion

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + this.transform.rotation * localHitCentrePoint, hitAreaRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector3.right);
    }

    #endregion

    #region Service Methods

    public override List<GameObject> GetAllCardsList() {
        List<GameObject> cards = new List<GameObject>();
        if (CardShape)
            cards.Add(CardShape.Prefab);
        if (CardMemory)
            cards.Add(CardMemory.Prefab);
        if (CardEff)
            cards.Add(CardEff.Prefab);
        return cards;
    }

    //private void SendBulletsToMainData() {
    //    MainData.ClipBullets = clipActualBullets;
    //    MainData.PocketBullets = pocketActualBullets;
    //}

    #endregion

    #region Overrided Methods

    private void Start() {
        InitializeStandardGunCardProps();
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
        if (CardShape != null)
            InstallCard(CardShape);
        if (CardMemory != null)
            InstallCard(CardMemory);
        if (CardEff != null)
            InstallCard(CardEff);
    }
    protected override void GetCardsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            CardGun card = child.GetComponent<CardGun>();
            if (card != null) {
                InstallUnknownCard(card);
            }
        }
    }

    #endregion

    #region WorkingWithCards Methods
    protected void InitializeStandardGunCardProps() {
        StandardCardShapeProps = new CardMeleeShape.CardMeleeShapeProps();
        StandardCardMemoryProps = new CardMeleeMemory.CardMeleeMemoryProps();
        StandardCardEffProps = new CardEffect.CardGunEffectProps();
    }
    public bool InstallUnknownCard(CardGun card) {
        if (card != null) {
            if (card is CardMeleeShape) {
                InstallCard((CardMeleeShape)card);
            } else if (card is CardMeleeMemory) {
                InstallCard((CardMeleeMemory)card);
            } else if (card is CardEffect) {
                InstallCard((CardEffect)card);
            }
            return true;
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
        ////////////////
    }
    private bool RemoveCard(CardMelee card) {
        ///////////////
        return true;
    }

    #endregion

    #region Main Methods

    private void Shield() {
        //
    }
    private void Hit() {
        if (canAttack) {
            CardMeleeShape.CardMeleeShapeProps CardShapeProps;
            if (CardShape == null) {
                CardShapeProps = StandardCardShapeProps;
            } else {
                CardShapeProps = CardShape.Props;
            }
            CardMeleeMemory.CardMeleeMemoryProps CardMemoryProps;
            if (CardMemory == null) {
                CardMemoryProps = StandardCardMemoryProps;
            } else {
                CardMemoryProps = CardMemory.Props;
            }

            Vector3 worldCentrePoint = this.transform.position + this.transform.rotation * localHitCentrePoint;
            Collider2D[] cols = Physics2D.OverlapCircleAll(worldCentrePoint, hitAreaRadius);
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
            canAttack = false;
            SetReliefTimer(1 / CardShapeProps.AttackSpeedMultiplier);
            Debug.Log(TAG + "Hit (" + actualHitCounter + " target" + ((actualHitCounter == 1) ? "" :"s") + ")");
        }
    }

    #endregion

    #region Inner Classes

    public new class Info : Weapon.Info {

    }

    #endregion
}
