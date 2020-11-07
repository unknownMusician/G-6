using G6.Characters;
using G6.UI;
using G6.Weapons.Cards;
using UnityEngine;

namespace G6.Weapons {
    public class Bullet : MonoBehaviour {

        const string TAG = "Bullet: ";

        #region Properties

        public EncyclopediaObject EncyclopediaObject => gameObject.GetComponent<EncyclopediaObject>();

        #endregion

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
        private int piercingCount = 0;
        private CardEffect.NestedProps effectProps;

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
            int magnettingLayerMask = 0,
            CardEffect.NestedProps effectProps = null) {

            this.damage = dmg;
            this.ricochet = ricochet;
            if (this.piercing = piercing)
                piercingCount = 1;
            this.homing = homing;
            this.teleporting = teleporting;
            this.magnet = magnet;
            this.enemy = enemyLayerMask;
            this.magnetting = magnettingLayerMask;
            this.effectProps = effectProps;
            Prepare();
        }

        public void SetParams(float dmg, CardGunFly.NestedProps bulletProps, CardEffect.NestedProps effectProps = null) {

            this.damage = dmg;
            this.ricochet = bulletProps.Ricochet;
            if (this.piercing = bulletProps.Piercing)
                piercingCount = 1;
            this.homing = bulletProps.Homing;
            this.teleporting = bulletProps.Teleporting;
            this.magnet = bulletProps.Magnet;
            this.enemy = bulletProps.Enemy;
            this.magnetting = bulletProps.Magnetting;
            this.effectProps = effectProps;
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

        private void OnTriggerEnter2D(Collider2D collider) {
            if (!collider.isTrigger) {
                // Damage:
                var cb = collider.gameObject.GetComponent<CharacterBase>();
                if (cb != null) {
                    if (effectProps != null)
                        cb.TakeDamage(rb.velocity.normalized * damage, effectProps);
                    else
                        cb.TakeDamage(rb.velocity.normalized * damage);
                }
                // Collide:
                if (piercingCount <= 0) {
                    if (ricochet) {
                        // jump
                        Vector3 closestPoint = collider.ClosestPoint(transform.position);
                        var moveRotation = Quaternion.FromToRotation(rb.velocity, closestPoint - transform.position);
                        rb.velocity = -(moveRotation * (moveRotation * rb.velocity));
                    } else {
                        Destroy(this.gameObject);
                    }
                } else {
                    piercingCount--;
                }
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
}