using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : EncyclopediaObject {

    const string TAG = "Bullet: ";

    #region Public Variables

    [SerializeField]
    private Rigidbody2D rb = null;

    #endregion

    #region Private Variables

    private float damage;

    private bool ricochet;
    private bool piercing;
    private bool homing;
    private bool teleporting;
    private bool magnet;
    private LayerMask whatIsAlive;
    private LayerMask enemy;
    private LayerMask magnetting;

    private GameObject aim;
    private int piercingCount;

    #endregion

    #region Setters

    public void SetParams(
        float dmg,
        bool ricochet = false,
        bool piercing = false,
        bool homing = false,
        bool teleporting = false,
        bool magnet = false,
        int enemyLayerMask = 0,
        int magnettingLayerMask = 0) {

        this.damage = dmg;
        this.ricochet = ricochet;
        this.piercing = piercing;
        this.homing = homing;
        this.teleporting = teleporting;
        this.magnet = magnet;
        this.enemy = enemyLayerMask;
        this.magnetting = magnettingLayerMask;
        Prepare();
    }

    public void SetParams(float dmg, CardGunFly.NestedProps bulletProps) {

        this.damage = dmg;
        this.ricochet = bulletProps.Ricochet;
        this.piercing = bulletProps.Piercing;
        this.homing = bulletProps.Homing;
        this.teleporting = bulletProps.Teleporting;
        this.magnet = bulletProps.Magnet;
        this.enemy = bulletProps.Enemy;
        this.magnetting = bulletProps.Magnetting;
        Prepare();
    }

    #endregion

    #region Overrided Methods

    private void Start() {
        Prepare();
    }

    private void Update() {
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg);

        if (homing && aim != null) {

            Vector2 dist = aim.transform.position - this.transform.position;
            Vector2 neededSpeed = dist.normalized * 30;
            Debug.Log(TAG + dist);
            Vector2 force = (neededSpeed - rb.velocity) * 2;
            rb.AddForce(force);
        }

        if (magnet) {
            Collider2D[] cols = new Collider2D[10];
            Physics2D.OverlapCircleNonAlloc(this.transform.position, 10, cols, magnetting);

            foreach (Collider2D col in cols) {
                if (col != null && col.GetComponent<Rigidbody2D>() != null) {

                    Vector2 dist = (this.transform.position - col.transform.position);
                    Vector2 force = (dist.normalized * 20 - col.GetComponent<Rigidbody2D>().velocity) * 2;
                    col.GetComponent<Rigidbody2D>().AddForce((force.normalized / Mathf.Pow(dist.magnitude, 2)) * 15);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        var cb = collision.gameObject.GetComponent<CharacterBase>();
        if (cb != null) {
            cb.TakeDamage(rb.velocity.normalized * damage);
            Destroy(this.gameObject);
            return;
        }
        if (!ricochet) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        piercingCount--;
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (piercingCount <= 0) {
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    #endregion

    #region Service Methods

    private void Prepare() {
        if (ricochet) {
            PhysicsMaterial2D bulletPhMat = new PhysicsMaterial2D();
            bulletPhMat.bounciness = 1;
            this.GetComponent<CircleCollider2D>().sharedMaterial = bulletPhMat;
        }

        if (homing) {
            aim = Physics2D.OverlapCircle(transform.position, 20, enemy)?.gameObject;
        }

        if (piercing) {
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    #endregion
}
