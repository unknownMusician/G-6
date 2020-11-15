using UnityEngine;
using System;
using System.Collections;
using G6.Environment.Interactables.Base;
using G6.UI;
using G6.Characters;
using G6.Weapons.Cards;

namespace G6.Weapons {
    public abstract class Weapon : InteractableBase {

        const string TAG = "Weapon: ";

        #region Actions
        public Action OnAttackAction;
        public Action OnInstallCardAction;
        #endregion

        #region Properties

        public EncyclopediaObject EncyclopediaObject => gameObject.GetComponent<EncyclopediaObject>();
        protected Weapon.State _weaponState;
        protected virtual Weapon.State WeaponState {
            get => _weaponState;
            set {
                _weaponState = value;
                switch (WeaponState) {
                    case State.Main:
                        friend = Character;
                        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Animator.SetBool("secondState", false);
                        WeaponCollider.enabled = false;
                        break;
                    case State.Alt:
                        friend = Character;
                        this.transform.localRotation = Quaternion.Euler(0, 0, -90);
                        Animator.SetBool("secondState", true);
                        WeaponCollider.enabled = false;
                        break;
                    case State.Throwed:
                        WeaponCollider.enabled = true;
                        transform.SetParent(null); // unparented weapon

                        EnablePhysics();
                        transform.rotation = Quaternion.identity;
                        transform.localScale = Vector3.one;
                        // todo
                        break;
                    case State.Levitating:
                        WeaponCollider.enabled = true;
                        DisablePhysics();
                        transform.SetParent(Instantiate(WeaponHolder, this.gameObject.transform.position, Quaternion.identity).transform);
                        // todo
                        break;
                }
            }
        }
        protected abstract bool CanAttack { get; set; }
        protected GameObject Character => transform.parent?.parent?.parent?.gameObject;
        protected GameObject WeaponHolder => Resources.Load<GameObject>("Prefabs/Weapons/Other/WeaponHolder");
        protected Rigidbody2D Rigidbody { get; set; } = null;
        protected SpriteRenderer SpriteRenderer { get; set; } = null;
        protected Animator Animator { get; set; } = null;
        protected Collider2D WeaponCollider { get; set; } = null;
        protected Inventory Inventory { get; set; } = null;

        // UI 
        public abstract Card CardSlot1 { get; }
        public abstract Card CardSlot2 { get; }
        public abstract Card CardSlot3 { get; }

        #endregion

        #region Public Variables

        [SerializeField]
        protected float throwHitDamage = 10;

        #endregion

        #region Private Variables

        protected GameObject friend = null;

        protected bool canAttack = true;
        protected bool attackButtonHold = false;

        #endregion

        #region Abstract Methods

        public abstract void Attack();
        public void AttackPress() {
            attackButtonHold = true;
        }
        public void AttackRelease() {
            attackButtonHold = false;
        }
        protected abstract void InstallCardsFromChildren();
        public abstract bool InstallUnknownCard(Card card);
        public abstract bool UninstallUnknownCard(Card card);

        #endregion

        #region Service Methods

        protected IEnumerator ReliefTimer(float time) {
            yield return new WaitForSeconds(time);
            CanAttack = true;
        }
        public void EnablePhysics() {
            Rigidbody.bodyType = RigidbodyType2D.Dynamic; // "enabled" Rigidbody2D
        }
        public void DisablePhysics() {
            Rigidbody.bodyType = RigidbodyType2D.Kinematic; // "disabled" Rigidbody2D
            Rigidbody.velocity = Vector2.zero;
            Rigidbody.angularVelocity = 0;
            transform.localRotation = Quaternion.identity;
        }

        #endregion

        #region Overrided Methods

        protected void Awake() {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
            WeaponCollider = GetComponent<CircleCollider2D>();
            Inventory = GetComponentInParent<Inventory>();
        }

        protected void Start() {
            StartCoroutine(WaitForFixedStart());
        }

        protected IEnumerator WaitForFixedStart() {
            yield return new WaitForFixedUpdate();
            WeaponState = (GetComponentInParent<CharacterBase>() != null) ? State.Main : State.Throwed;
        }

        protected void Update() { if (attackButtonHold) Attack(); }

        protected void OnTriggerEnter2D(Collider2D collider) {
            if (_weaponState == State.Throwed && collider.gameObject != friend) {
                collider.gameObject.GetComponent<CharacterBase>()?.TakeDamage(Rigidbody.velocity.normalized * throwHitDamage);
                WeaponState = State.Levitating;
            }
        }

        #endregion

        #region Main Methods

        public void ChangeState() => WeaponState = (WeaponState == State.Alt) ? State.Main : State.Alt;

        public void Throw(GameObject whoThrowed, Vector2 direction) {
            WeaponState = State.Throwed;
            friend = whoThrowed;
            Rigidbody.velocity += direction; // "throwed" the weapon
            Rigidbody.AddTorque(-Mathf.Sign(direction.x) * direction.magnitude * 150f);
        }

        public void PrepareToPostPickUp() {
            // transform set
            transform.localPosition = new Vector3(0.2f, 0.2f);
            transform.localScale = Vector3.one;
            // rigidbody set
            DisablePhysics();
            WeaponState = State.Main;
        }

        #endregion

        public override bool Interact(GameObject whoInterracted) {
            // To-Do
            if (WeaponState == State.Levitating || WeaponState == State.Throwed) {
                var cb = whoInterracted?.GetComponent<CharacterBase>();
                if (cb != null && cb.transform != Character) {
                    return cb.Inventory.Weapons.Add(this);
                }
            }
            return false;
        }

        public enum State {
            Main,
            Alt,
            Throwed,
            Levitating
        }
    }
}