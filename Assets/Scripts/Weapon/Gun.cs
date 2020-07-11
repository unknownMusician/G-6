using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class Gun : Weapon {

    const string TAG = "Gun: ";

    #region Properties

    public int ClipMaxBullets {
        get { return clipMaxBullets; }
        private set {
            clipMaxBullets = value;
            CheckNSendBullets();
        }
    }
    public int PocketActualBullets {
        get { return pocketActualBullets; }
        private set {
            pocketActualBullets = value;
            CheckNSendBullets();
        }
    }
    public int ClipActualBullets {
        get { return clipActualBullets; }
        private set {
            clipActualBullets = value;
            CheckNSendBullets();
        }
    }

    public override List<GameObject> AllCardPrefabList {
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

    private bool CanAttack { get { return canAttack; } set { canAttack = value; if (!value) SetReliefTimer(1 / ActualCardGenProps.FireRateMultiplier); } }
    private Vector3 WorldFirePoint { get { return transform.position + transform.rotation * localFirePoint; } }

    private CardGunGen.CardGunGenProps ActualCardGenProps { get { return CardGen == null ? StandardCardGenProps : CardGen.Props; } }
    private CardGunFly.CardGunFlyProps ActualCardFlyProps { get { return CardFly == null ? StandardCardFlyProps : CardFly.Props; } }

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
    private float standardDamage;
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

    protected CardGunGen.CardGunGenProps StandardCardGenProps = new CardGunGen.CardGunGenProps();
    protected CardGunFly.CardGunFlyProps StandardCardFlyProps = new CardGunFly.CardGunFlyProps();
    protected CardEffect.CardGunEffectProps StandardCardEffProps = new CardEffect.CardGunEffectProps();

    private bool isLoaded = true;

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

    private void InstantiateBullet() {
        GameObject blt = Instantiate(bullet, WorldFirePoint, transform.rotation);
        Destroy(blt, bulletLifeTime * ActualCardGenProps.ShotRangeMultiplier);
        blt.GetComponent<Bullet>().SetParams(standardDamage, ActualCardFlyProps);
        Vector3 characterVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
        blt.GetComponent<Rigidbody2D>().velocity = characterVelocity + Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
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
            InstallUnknownCard(this.transform.GetChild(i).gameObject.GetComponent<CardGun>()); // there will already be null-check
        }
    }

    #endregion

    #region WorkingWithCards Methods

    public bool InstallUnknownCard(CardGun card) {
        // switch-case does not fit here
        if (card is CardGunGen) {
            return InstallCard((CardGunGen)card);
        } else if (card is CardGunFly) {
            return InstallCard((CardGunFly)card);
        } else if (card is CardEffect) {
            return InstallCard((CardEffect)card);
        }
        return false;
    }
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
    private void PrepareCardforInstall(CardGun cardGen) {
        // To-Do
    }
    private bool RemoveCard(CardGun card) {
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
        if (CanAttack && isLoaded) {
            int bulletsPerShot = Mathf.Min(ActualCardGenProps.BulletsPerShotAdder + 1, ClipActualBullets);
            for (int i = 0; i < bulletsPerShot; i++) {
                InstantiateBullet();
                ClipActualBullets--;
            }
            CanAttack = false;
            Debug.Log(TAG + "Bullets: " + ClipActualBullets + "/" + PocketActualBullets);
        }
    }

    #endregion

    #region WorkingWithBullets methods

    private void CheckNSendBullets() {
        isLoaded = ClipActualBullets > 0;

        ((Gun.Info)MainData.ActiveWeapon).ActualClipBullets = ClipActualBullets;
        ((Gun.Info)MainData.ActiveWeapon).ActualPocketBullets = PocketActualBullets;
        MainData.ActionWeapons();
    }
    public void Reload() {
        int bulletsNeeded = ClipMaxBullets - ClipActualBullets;
        if (bulletsNeeded <= PocketActualBullets) {
            ClipActualBullets += bulletsNeeded;
            PocketActualBullets -= bulletsNeeded;
        } else {
            ClipActualBullets += PocketActualBullets;
            PocketActualBullets = 0;
        }
    }

    #endregion

    #region Inner Classes

    public new class Info : Weapon.Info {
        public int ActualClipBullets;
        public int ActualPocketBullets;

        public Info(GameObject weaponPrefab, List<GameObject> cardPrefabs, int actualClipBullets, int actualPocketBullets)
            : base(weaponPrefab, cardPrefabs) {
            this.ActualClipBullets = actualClipBullets;
            this.ActualPocketBullets = actualPocketBullets;
        }
    }

    #endregion
}
