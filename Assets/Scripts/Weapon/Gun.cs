using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public class Gun : Weapon {

    readonly string TAG = "Gun: ";

    #region Actions
    public Action OnAttackAction;
    public Action OnInstallCardAction;
    #endregion

    #region Public Variables

    [Space]
    [Space]
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private Transform firePoint = null;

    [SerializeField]
    private float bulletSpeed = 20;
    [SerializeField]
    private float spread = 5;
    [SerializeField]
    private float bulletLifeTime = 1;
    [Space]
    [SerializeField]
    private int clipMaxBullets = 9;
    [SerializeField]
    private int pocketActualBullets = 30;
    [SerializeField]
    private int clipActualBullets = 5;

    [Space]
    [SerializeField]
    protected CardGunGen CardGen;
    [Space]
    [SerializeField]
    protected CardGunFly CardFly;
    [Space]
    [SerializeField]
    protected CardGunEffect CardEff;

    #endregion

    #region Private Variables

    protected CardGunGen.CardGunGenProps StandardCardGenProps;
    protected CardGunFly.CardGunFlyProps StandardCardFlyProps;
    protected CardGunEffect.CardGunEffectProps StandardCardEffProps;

    private bool isLoaded = true;

    #endregion

    #region Overrided Methods

    private void Start() {
        InitializeStandardGunCardProps();
        GetModulesFromChildren();
        InstallModCards();
    }
    public override void Attack() {
        if (secondState) {
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
    protected override void GetModulesFromChildren() {
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
        StandardCardEffProps = new CardGunEffect.CardGunEffectProps();
    }
    public bool InstallUnknownCard(CardGun card) {
        if (card != null) {
            if (card is CardGunGen) {
                InstallCard((CardGunGen)card);
            } else if (card is CardGunFly) {
                InstallCard((CardGunFly)card);
            } else if (card is CardGunEffect) {
                InstallCard((CardGunEffect)card);
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
    public bool InstallCard(CardGunEffect cardEff) {
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

    #region Damaging Methods

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
            for (int i = 0; i <= CardGenProps.BulletsPerShotAdder; i++) {
                GameObject blt = Instantiate(bullet, firePoint.position, firePoint.rotation);
                Destroy(blt, bulletLifeTime * CardGenProps.ShotRangeMultiplier);
                blt.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
                    * Vector2.right * bulletSpeed;
                blt.GetComponent<Bullet>().SetParams(CardFlyProps);
            }
            clipActualBullets -= CardGenProps.BulletsPerShotAdder + 1;
            CheckBullets();
            canAttack = false;
            SetReliefTimer(1 / CardGenProps.FireRateMultiplier);
            Debug.Log(TAG + "Bullets: " + clipActualBullets + "/" + pocketActualBullets);
        }
    }

    #endregion

    #region WorkingWithBullets methods

    private void CheckBullets() {
        if (clipActualBullets > 0) {
            isLoaded = true;
        } else {
            isLoaded = false;
        }
    }
    public override void Reload() {
        int bulletsNeeded = clipMaxBullets - clipActualBullets;
        if (bulletsNeeded <= pocketActualBullets) {
            clipActualBullets += bulletsNeeded;
            pocketActualBullets -= bulletsNeeded;
        } else {
            clipActualBullets += pocketActualBullets;
            pocketActualBullets = 0;
        }
        CheckBullets();
    }

    #endregion
}
