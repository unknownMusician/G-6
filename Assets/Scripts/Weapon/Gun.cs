using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class Gun : Weapon {

    const string TAG = "Gun: ";

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

    protected CardGunGen.CardGunGenProps StandardCardGenProps;
    protected CardGunFly.CardGunFlyProps StandardCardFlyProps;
    protected CardEffect.CardGunEffectProps StandardCardEffProps;

    private bool isLoaded = true;

    #endregion

    #region Gizmos

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * localFirePoint, 0.2f);
        Gizmos.DrawRay(transform.position + transform.rotation * localFirePoint, transform.rotation * Vector3.right);
        Gizmos.color = Color.gray;
        Gizmos.DrawRay(transform.position, transform.rotation * Vector3.right);
    }

    #endregion

    #region Service Methods

    public override List<GameObject> GetAllCardsList() {
        List<GameObject> cards = new List<GameObject>();
        if (CardGen)
            cards.Add(CardGen.Prefab);
        if (CardFly)
            cards.Add(CardFly.Prefab);
        if (CardEff)
            cards.Add(CardEff.Prefab);
        return cards;
    }

    private void SendBulletsToMainData() {
        MainData.ClipBullets = clipActualBullets;
        MainData.PocketBullets = pocketActualBullets;
    }

    #endregion

    #region Overrided Methods

    private void Start() {
        InitializeStandardGunCardProps();
        GetCardsFromChildren();
        InstallModCards();
        SendBulletsToMainData();
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
        if (CardGen != null)
            InstallCard(CardGen);
        if (CardFly != null)
            InstallCard(CardFly);
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
        StandardCardGenProps = new CardGunGen.CardGunGenProps();
        StandardCardFlyProps = new CardGunFly.CardGunFlyProps();
        StandardCardEffProps = new CardEffect.CardGunEffectProps();
    }
    public bool InstallUnknownCard(CardGun card) {
        if (card != null) {
            if (card is CardGunGen) {
                InstallCard((CardGunGen)card);
            } else if (card is CardGunFly) {
                InstallCard((CardGunFly)card);
            } else if (card is CardEffect) {
                InstallCard((CardEffect)card);
            }
            return true;
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
        ////////////////
    }
    private bool RemoveCard(CardGun card) {
        ///////////////
        return true;
    }

    #endregion

    #region Main Methods

    private void Hit() {
        animator.SetTrigger("hit");
    }
    private void Shoot() {
        if (canAttack && isLoaded) {
            CardGunGen.CardGunGenProps CardGenProps;
            if (CardGen == null) {
                CardGenProps = StandardCardGenProps;
            } else {
                CardGenProps = CardGen.Props;
            }
            CardGunFly.CardGunFlyProps CardFlyProps;
            if (CardFly == null) {
                CardFlyProps = StandardCardFlyProps;
            } else {
                CardFlyProps = CardFly.Props;
            }
            int bulletsPerShot = Mathf.Min(CardGenProps.BulletsPerShotAdder + 1, clipActualBullets);
            for (int i = 0; i < bulletsPerShot; i++) {
                GameObject blt = Instantiate(bullet, transform.position + transform.rotation * localFirePoint, transform.rotation);
                Destroy(blt, bulletLifeTime * CardGenProps.ShotRangeMultiplier);
                blt.GetComponent<Bullet>().SetParams(standardDamage, CardFlyProps);
                Vector3 characterVelocity = transform.parent.parent.GetComponent<Rigidbody2D>().velocity;
                blt.GetComponent<Rigidbody2D>().velocity = characterVelocity + Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
                    * Vector2.right * bulletSpeed;
                clipActualBullets--;
            }
            CheckNSendBullets();
            canAttack = false;
            SetReliefTimer(1 / CardGenProps.FireRateMultiplier);
            Debug.Log(TAG + "Bullets: " + clipActualBullets + "/" + pocketActualBullets);
        }
    }

    #endregion

    #region WorkingWithBullets methods

    private void CheckNSendBullets() {
        if (clipActualBullets > 0) {
            isLoaded = true;
        } else {
            isLoaded = false;
        }
        SendBulletsToMainData();
    }
    public void Reload() {
        int bulletsNeeded = clipMaxBullets - clipActualBullets;
        if (bulletsNeeded <= pocketActualBullets) {
            clipActualBullets += bulletsNeeded;
            pocketActualBullets -= bulletsNeeded;
        } else {
            clipActualBullets += pocketActualBullets;
            pocketActualBullets = 0;
        }
        CheckNSendBullets();
    }

    #endregion

    #region Inner Classes

    public class GunInfo {
        public GameObject WeaponPrefab;
        public List<GameObject> CardPrefabs;
        public int ActualClipBullet;
        public int ActualPocketBullet;
    }

    #endregion
}
