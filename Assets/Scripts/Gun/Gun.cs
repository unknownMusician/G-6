using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Gun : Weapon {
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Collider2D weaponCollider;
    [SerializeField]
    private List<GunModule> modules;
    [SerializeField]
    private GameObject bullet = null;
    [SerializeField]
    private Transform firePoint = null;

    [SerializeField]
    private float hitStrenght = 10;
    [SerializeField]
    private float bulletSpeed = 20;
    [SerializeField]
    private float spread = 5;

    [SerializeField]
    private List<Sprite> spriteArray;
    private Dictionary<int, Sprite> sprites;

    private GunProps gunProps;
    private BulletProps bulletProps;

    private Timer timer;

    private bool canShoot = true;
    private bool secondState = false;

    private void Start() {
        sprites = new Dictionary<int, Sprite>();
        GetModulesInChildren();
        InstallMods();
        FillDictionary();
        SetSprite();
    }
    private void SetSprite() {
        int finNum = 0;
        foreach (GunModule mod in modules) {
            if (mod is GunModuleGenMain) {
                finNum += ((GunModuleGenMain)mod).id;
            }
        }
        string key = finNum.ToString();
        for(int i = 0; i < 4; i++) {
            if (key[i] == 3) {
                key = key.Substring(0, i) + "2" + key.Substring(i + 1);
            }
        }
        this.spriteRenderer.sprite = sprites[int.Parse(key)];
    }
    private void InstallMods() {
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
    private void FillDictionary() {
        sprites.Add(1111, spriteArray[0]);
        sprites.Add(0, spriteArray[1]);
        sprites.Add(1000, spriteArray[2]);
        sprites.Add(100, spriteArray[3]);
        sprites.Add(10, spriteArray[4]);
        sprites.Add(1, spriteArray[5]);
        sprites.Add(1110, spriteArray[6]);
        sprites.Add(2000, spriteArray[7]);
        sprites.Add(1100, spriteArray[8]);
        sprites.Add(1010, spriteArray[9]);
        sprites.Add(1001, spriteArray[10]);
        sprites.Add(4000, spriteArray[11]);
        sprites.Add(1101, spriteArray[12]);
        sprites.Add(2200, spriteArray[13]);
        sprites.Add(200, spriteArray[14]);
        sprites.Add(0110, spriteArray[15]);
        sprites.Add(0101, spriteArray[16]);
        sprites.Add(400, spriteArray[17]);
        sprites.Add(1011, spriteArray[18]);
        sprites.Add(2020, spriteArray[19]);
        sprites.Add(220, spriteArray[20]);
        sprites.Add(20, spriteArray[21]);
        sprites.Add(11, spriteArray[22]);
        sprites.Add(40, spriteArray[23]);
        sprites.Add(111, spriteArray[24]);
        sprites.Add(2002, spriteArray[25]);
        sprites.Add(202, spriteArray[26]);
        sprites.Add(22, spriteArray[27]);
        sprites.Add(2, spriteArray[28]);
        sprites.Add(4, spriteArray[29]);
    }
    private void GetModulesInChildren() {
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
    }
    public override void Attack() {
        if (secondState) {
            Hit();
        } else {
            Shoot();
        }
    }
    public override void ChangeState() {
        if (secondState) {
            secondState = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("secondState", false);
            weaponCollider.enabled = false;

        } else {
            secondState = true;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            animator.SetBool("secondState", true);
            weaponCollider.enabled = true;
        }
    }
    public override Collider2D GetWeaponCollider() {
        return weaponCollider;
    }
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
