using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Gun : Weapon {

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
    private int clipSize = 9;
    [SerializeField]
    private int actualStock = 30;
    [SerializeField]
    private int actualBullets = 5;

    [Space]
    [SerializeField]
    protected CardGunGen cardGen;
    [Space]
    [SerializeField]
    protected CardGunFly cardFly;
    [Space]
    [SerializeField]
    protected CardGunEffect cardEff;

    private bool isLoaded = true;

    private void Start() {
        GetModulesFromChildren();
        InstallModCards();
    }
    public override void Attack() {
        if (secondState) {
            Hit();
        } else {
            Shoot();
        }
    }
    protected override void InstallModCards() {
        if (cardGen != null)
            InstallCard(cardGen);
        if (cardFly != null)
            InstallCard(cardFly);
        if (cardEff != null)
            InstallCard(cardEff);
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
            RemoveCard(this.cardGen);
            PrepareCardforInstall(cardGen);
            this.cardGen = cardGen;
            return true;
        }
        return false;
    }
    public bool InstallCard(CardGunFly cardFly) {
        if (cardFly != null) {
            RemoveCard(this.cardFly);
            PrepareCardforInstall(cardFly);
            this.cardFly = cardFly;
            return true;
        }
        return false;
    }
    public bool InstallCard(CardGunEffect cardEff) {
        if(cardEff != null) {
            ////////////////
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
    
    private void Hit() {
        animator.SetTrigger("hit");
    }
    private void Shoot() {
        if (canAttack && isLoaded) {
            for (int i = 0; i < cardGen.Props.BulletsPerShotAdder; i++) {
                GameObject blt = Instantiate(bullet, firePoint.position, firePoint.rotation);
                blt.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
                    * Vector2.right * bulletSpeed;
                blt.GetComponent<Bullet>().SetParams(cardFly.Props);
            }
            actualBullets -= cardGen.Props.BulletsPerShotAdder;
            CheckBullets();
            canAttack = false;
            SetReliefTimer(1 / cardGen.Props.FireRateMultiplier);
            Debug.Log(actualBullets + "/" + actualStock);
        }
    }
    private void CheckBullets() {
        if (actualBullets > 0) {
            isLoaded = true;
        } else {
            isLoaded = false;
        }
    }
    public override void Reload() {
        if (actualStock >= clipSize) {
            actualBullets = clipSize;
            actualStock -= clipSize;
        } else {
            actualBullets = actualStock;
            actualStock = 0;
        }
        CheckBullets();
    }
}
