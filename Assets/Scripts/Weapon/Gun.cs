using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {

    const string TAG = "Gun: ";

    #region Properties

    public int MaxClipBullets {
        get => clipMaxBullets;
        private set {
            if (clipMaxBullets != value) {
                clipMaxBullets = value;
            }
        }
    }
    public int ActualPocketBullets {
        get => pocketActualBullets;
        private set {
            if (pocketActualBullets != value) {
                pocketActualBullets = value;
                MainData.ActionGunBulletsChange?.Invoke();
            }
        }
    }
    public int ActualClipBullets {
        get => clipActualBullets;
        private set {
            if (clipActualBullets != value) {
                clipActualBullets = value;
                MainData.ActionGunBulletsChange?.Invoke();
            }
        }
    }

    //////////

    public CardGunGen CardGen { get => cardGen; private set => cardGen = value; }
    public CardGunFly CardFly { get => cardFly; private set => cardFly = value; }
    public CardEffect CardEff { get => cardEff; private set => cardEff = value; }

    // UI 
    public override Card CardSlot1 => CardGen;
    public override Card CardSlot2 => CardFly;
    public override Card CardSlot3 => CardEff;

    //////////

    protected override bool CanAttack {
        get => canAttack;
        set { if (!(canAttack = value)) SetReliefTimer(1 / ActualCardGenProps.FireRateMultiplier); }
    }
    protected bool IsLoaded => ActualClipBullets > 0;
    private Vector3 WorldFirePoint => transform.position + transform.rotation * localFirePoint;

    private CardGunGen.NestedProps ActualCardGenProps => CardGen?.Props ?? StandardCardGenProps;
    private CardGunFly.NestedProps ActualCardFlyProps => CardFly?.Props ?? StandardCardFlyProps;
    private CardEffect.NestedProps ActualCardEffectProps => CardEff?.Props ?? StandardCardEffProps;

    #endregion

    #region Public Variables

    [Space]
    [Space]
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private Vector3 localFirePoint = Vector3.right;

    [Space]
    [Space]
    [SerializeField]
    private float standardDamage = 10f;
    [SerializeField]
    private float bulletSpeed = 20;
    [SerializeField]
    private float spread = 5;
    [SerializeField]
    private float bulletLifeTime = 1;

    [Space]
    [Space]
    [SerializeField]
    private int clipMaxBullets = 9;
    [SerializeField]
    private int pocketActualBullets = 30;
    [SerializeField]
    private int clipActualBullets = 5;

    [Space]
    [Space]
    [SerializeField]
    private CardGunGen cardGen;
    [Space]
    [SerializeField]
    private CardGunFly cardFly;
    [Space]
    [SerializeField]
    private CardEffect cardEff;

    #endregion

    #region Private Variables

    protected CardGunGen.NestedProps StandardCardGenProps = new CardGunGen.NestedProps();
    protected CardGunFly.NestedProps StandardCardFlyProps = new CardGunFly.NestedProps();
    protected CardEffect.NestedProps StandardCardEffProps = new CardEffect.NestedProps();

    #endregion

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(WorldFirePoint, 0.2f);
        Gizmos.DrawRay(WorldFirePoint, transform.rotation * Vector3.right);
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector3.right);
    }

    #endregion

    #region Service Methods

    private void InstantiateBullet(int index) {
        GameObject blt = Instantiate(bullet, WorldFirePoint, transform.rotation);
        Destroy(blt, bulletLifeTime * ActualCardGenProps.ShotRangeMultiplier);
        blt.GetComponent<Bullet>().SetParams(standardDamage, ActualCardFlyProps);
        Vector3 characterVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
        blt.GetComponent<Rigidbody2D>().velocity = characterVelocity + Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + (Mathf.Pow(-1, index) * index / 2 * spread))
            * Vector2.right * bulletSpeed;
    }

    #endregion

    #region Overrided Methods

    private void Start() {
        InstallCardsFromChildren();
    }
    public override void Attack() {
        if (state == State.Alt) {
            Hit();
        } else {
            Shoot();
        }
        OnAttackAction?.Invoke();
    }
    protected override void InstallCardsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<Card>()); // there will already be null-check
        }
    }

    public override bool InstallUnknownCard(Card card) => InstallCard(card as CardGunGen) || InstallCard(card as CardGunFly) || InstallCard(card as CardEffect);
    public override bool UninstallUnknownCard(Card card) {
        if (card != null) {
            bool answer = false;
            if (answer = card == CardGen)
                CardGen = null;
            else if (answer = card == CardFly)
                CardFly = null;
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

    public bool InstallCard(CardGunGen cardGen) {
        if (cardGen != null) {
            UninstallUnknownCard(CardGen);
            PrepareCardforInstall(cardGen);
            cardGen.gameObject.transform.SetParent(transform);
            CardGen = cardGen;
            MainData.Inventory.Cards.Remove(cardGen);
            MainData.Inventory.Cards = MainData.Inventory.Cards;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardGunFly cardFly) {
        if (cardFly != null) {
            UninstallUnknownCard(this.CardFly);
            PrepareCardforInstall(cardFly);
            cardFly.gameObject.transform.SetParent(transform);
            this.CardFly = cardFly;
            MainData.Inventory.Cards.Remove(cardFly);
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
            cardEff.gameObject.transform.SetParent(transform);
            this.CardEff = cardEff;
            MainData.Inventory.Cards.Remove(cardEff);
            MainData.Inventory.Cards = MainData.Inventory.Cards;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    private void PrepareCardforInstall(Card cardGen) {
        // To-Do
    }

    #endregion

    #region WorkingWithBullets methods

    public void Reload() {
        int bulletsNeeded = MaxClipBullets - ActualClipBullets;
        if (bulletsNeeded <= ActualPocketBullets) {
            ActualClipBullets += bulletsNeeded;
            ActualPocketBullets -= bulletsNeeded;
        } else {
            ActualClipBullets += ActualPocketBullets;
            ActualPocketBullets = 0;
        }
    }

    #endregion

    #region Main Methods

    private void Hit() {
        // To-Do
        animator.SetTrigger("hit");
    }
    private void Shoot() {
        if (CanAttack && IsLoaded) {
            int bulletsPerShot = Mathf.Min(ActualCardGenProps.BulletsPerShotAdder + 1, ActualClipBullets);
            for (int i = 0; i < bulletsPerShot; i++) {
                InstantiateBullet(i);
                ActualClipBullets--;
            }
            CanAttack = false;
            Debug.Log(TAG + "Bullets: " + ActualClipBullets + "/" + ActualPocketBullets);
        }
    }

    #endregion
}
