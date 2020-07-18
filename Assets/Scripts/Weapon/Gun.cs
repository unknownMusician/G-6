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
                SendBullets();
            }
        }
    }
    public int ActualPocketBullets {
        get => pocketActualBullets;
        private set {
            if (pocketActualBullets != value) {
                pocketActualBullets = value;
                SendBullets();
            }
        }
    }
    public int ActualClipBullets {
        get => clipActualBullets;
        private set {
            if (clipActualBullets != value) {
                clipActualBullets = value;
                SendBullets();
            }
        }
    }

    public override List<GameObject> CardPrefabs {
        get {
            List<GameObject> cards = new List<GameObject>();
            if (CardGen)
                cards.Add(CardGen.Prefab);
            if (CardFly)
                cards.Add(CardFly.Prefab);
            if (CardEff)
                cards.Add(CardEff.Prefab);
            return cards;
        }
    }

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
    protected CardGunGen CardGen;
    [Space]
    [SerializeField]
    protected CardGunFly CardFly;
    [Space]
    [SerializeField]
    protected CardEffect CardEff;

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
        GetCardsFromChildren();
        InstallModCards();
    }
    public override void Attack() {
        if (state == State.Alt) {
            Hit();
        } else {
            Shoot();
        }
        OnAttackAction?.Invoke();
    }
    protected override void InstallModCards() {
        InstallCard(CardGen); // there will already be null-check
        InstallCard(CardFly); // there will already be null-check
        InstallCard(CardEff); // there will already be null-check
    }
    protected override void GetCardsFromChildren() {
        for (int i = 0; i < this.transform.childCount; i++) {
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<Card>()); // there will already be null-check
        }
    }

    #endregion

    #region WorkingWithCards Methods

    public bool InstallUnknownCard(Card card) => InstallCard(card as CardGunGen) || InstallCard(card as CardGunFly) || InstallCard(card as CardEffect);

    public bool InstallCard(CardGunGen cardGen) {
        if (cardGen != null) {
            RemoveCard(this.CardGen);
            PrepareCardforInstall(cardGen);
            this.CardGen = cardGen;
            OnInstallCardAction?.Invoke();
            return true;
        }
        return false;
    }
    public bool InstallCard(CardGunFly cardFly) {
        if (cardFly != null) {
            RemoveCard(this.CardFly);
            PrepareCardforInstall(cardFly);
            this.CardFly = cardFly;
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
        // To-Do
    }
    private bool RemoveCard(Card card) {
        // To-Do
        return false;
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

    #region WorkingWithBullets methods

    private void SendBullets() {
        ((Gun)MainData.ActiveWeapon).ActualClipBullets = ActualClipBullets;
        ((Gun)MainData.ActiveWeapon).ActualPocketBullets = ActualPocketBullets;
        MainData.ActionWeapons();
    }
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
}
