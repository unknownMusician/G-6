using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Gun : Weapon {
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Collider2D hitTrigger;
    [SerializeField]
    private List<GunModule> modules;

    [SerializeField]
    private float hitStrenght = 10;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float spread;

    private GunProps gunProps;
    private BulletProps bulletProps;

    private Timer timer;

    private bool canShoot = true;
    private bool secondState = false;

    private void SetTimer(float time) {
        // Create a timer with a two second interval.
        timer = new System.Timers.Timer(time * 1000);
        // Hook up the Elapsed event for the timer. 
        timer.Elapsed += SetCanShoot;
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    private void SetCanShoot(object sender, ElapsedEventArgs e) {
        canShoot = true;
    }

    private void Start() {
        this.gunProps = new GunProps();
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

    private void InstallMod(GunModuleGen modGen) {
        GunProps newProps = modGen.GetProps();
        this.gunProps.FireRate *= newProps.FireRate;
        this.gunProps.ShotBltAmt += newProps.ShotBltAmt;
        this.gunProps.AtkArea *= newProps.AtkArea;
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

    public void Attack() {
        if (secondState) {
            Hit();
        } else {
            Shoot();
        }
    }

    public void ChangeState() {
        if (secondState) {
            secondState = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("secondState", false);
            hitTrigger.enabled = false;

        } else {
            secondState = true;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            animator.SetBool("secondState", true);
            hitTrigger.enabled = true;
        }
    }

    public void Hit() {
        animator.SetTrigger("hit");
    }

    public void Shoot() {

        if (canShoot) {
            for (int i = 0; i < gunProps.ShotBltAmt; i++) {
                GameObject blt = Instantiate(bullet, firePoint.position, firePoint.rotation);
                blt.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(
                        transform.rotation.eulerAngles.x,
                        transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z + (Mathf.Pow(-1, i) * i / 2 * spread))
                    * Vector2.right * bulletSpeed;
                blt.GetComponent<Bullet>().SetParams(bulletProps);
            }
            canShoot = false;
            SetTimer(1 / gunProps.FireRate);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (crb != null) {
            Vector2 dist = collision.transform.position - this.transform.position;
            crb.velocity += dist.normalized * hitStrenght;
        }
    }
}
