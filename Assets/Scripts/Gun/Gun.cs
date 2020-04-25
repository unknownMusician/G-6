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

    private GunProps gunProps;
    private BulletProps bulletProps;

    private bool isLoaded = true;

    private void Start() {
        sprites = new Dictionary<int, Sprite>();
        GetModulesFromChildren();
        InstallMods();
        FillDictionary();
        //SetSprite();
    }
    //protected override void SetSprite() {
    //    int finNum = 0;
    //    foreach (GunModule mod in modules) {
    //        if (mod is GunModuleGenMain) {
    //            finNum += ((GunModuleGenMain)mod).id;
    //        }
    //    }
    //    string key = finNum.ToString();
    //    for(int i = 0; i < 4; i++) {
    //        if (key[i] == 3) {
    //            key = key.Substring(0, i) + "2" + key.Substring(i + 1);
    //        }
    //    }
    //    this.spriteRenderer.sprite = sprites[int.Parse(key)];
    //}
    public override void Attack() {
        if (secondState) {
            Hit();
        } else {
            Shoot();
        }
    }
    protected override void InstallMods() {
        foreach (GunModule mod in modules) {
            if (mod != null) {
                if (mod is GunModuleGen) {
                    GunModuleGen modGen = (GunModuleGen)mod;
                    InstallMod(modGen);
                } else if (mod is GunModuleFly) {
                    GunModuleFly modFly = (GunModuleFly)mod;
                    InstallMod(modFly);
                }
            }
        }
    }
    protected override void GetModulesFromChildren() {
        this.gunProps = new GunProps(1,1,1,1);
        this.bulletProps = new BulletProps();

        for (int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            GunModule mod = child.GetComponent<GunModule>();
            if (mod != null) {
                for (int j = 0; j < modules.Count; j++) {
                    if (modules[j] == null) {
                        modules[j] = mod;
                        break;
                    }
                }
            }
        }
    }
    private void InstallMod(GunModuleGen modGen) {
        GunProps newProps = modGen.GetProps();
        this.gunProps.FireRateMultiplier *= newProps.FireRateMultiplier;
        this.gunProps.BulletsPerShotAdder += newProps.BulletsPerShotAdder;
        this.gunProps.BulletMassMultiplier *= newProps.BulletMassMultiplier;
        this.gunProps.ShotRangeMultiplier *= newProps.ShotRangeMultiplier;
    }
    private void InstallMod(GunModuleFly modFly) {
        BulletProps newProps = modFly.GetProps();
        this.bulletProps.Homing = this.bulletProps.Homing || newProps.Homing;
        this.bulletProps.Magnet = this.bulletProps.Magnet || newProps.Magnet;
        this.bulletProps.Piercing = this.bulletProps.Piercing || newProps.Piercing;
        this.bulletProps.Ricochet = this.bulletProps.Ricochet || newProps.Ricochet;
        this.bulletProps.Teleporting = this.bulletProps.Teleporting || newProps.Teleporting;
        this.bulletProps.Enemy = newProps.Enemy;
        this.bulletProps.Magnetting = newProps.Magnetting;
    }
    private void Hit() {
        animator.SetTrigger("hit");
    }
    private void Shoot() {
        if (canAttack && isLoaded) {
            for (int i = 0; i < gunProps.BulletsPerShotAdder; i++) {
                GameObject blt = Instantiate(bullet, firePoint.position, firePoint.rotation);
                blt.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
                    * Vector2.right * bulletSpeed;
                blt.GetComponent<Bullet>().SetParams(bulletProps);
            }
            actualBullets -= gunProps.BulletsPerShotAdder;
            CheckBullets();
            canAttack = false;
            SetReliefTimer(1 / gunProps.FireRateMultiplier);
            Debug.Log(actualBullets + "/" + actualStock);
        }
    }
    private void CheckBullets() {
        if(actualBullets > 0) {
            isLoaded = true;
        } else {
            isLoaded = false;
        }
    }
    public override void Reload() {
        if(actualStock >= clipSize) {
            actualBullets = clipSize;
            actualStock -= clipSize;
        } else {
            actualBullets = actualStock;
            actualStock = 0;
        }
        CheckBullets();
    }
}
