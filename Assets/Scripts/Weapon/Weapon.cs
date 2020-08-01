using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public abstract class Weapon : BaseEnvironment {

    const string TAG = "Weapon: ";

    #region Actions
    public Action OnAttackAction;
    public Action OnInstallCardAction;
    #endregion

    #region Properties

    public EncyclopediaObject EncyclopediaObject => gameObject.GetComponent<EncyclopediaObject>();
    protected abstract bool CanAttack { get; set; }
    protected virtual Weapon.State WeaponState {
        get => state;
        set {
            state = value;
            Debug.Log(TAG + "Changed state: " + WeaponState);
            switch (WeaponState) {
                case State.Main:
                    this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    animator.SetBool("secondState", false);
                    break;
                case State.Alt:
                    this.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    animator.SetBool("secondState", true);
                    break;
            }
        }
    }

    // UI 
    public abstract Card CardSlot1 { get; }
    public abstract Card CardSlot2 { get; }
    public abstract Card CardSlot3 { get; }

    #endregion

    #region Public Variables

    [SerializeField]
    protected GameObject weaponHolder = null;
    [SerializeField]
    protected Rigidbody2D rigidBody = null;
    [SerializeField]
    protected SpriteRenderer spriteRenderer = null;
    [SerializeField]
    protected Animator animator = null;
    [SerializeField]
    protected Collider2D weaponCollider = null;

    [SerializeField]
    protected float throwHitDamage = 10;

    #endregion

    #region Private Variables

    protected GameObject friend;

    protected Timer timer;

    protected Weapon.State state;
    protected bool canAttack = true;
    public bool attackButtonHold;

    #endregion

    #region Abstract Methods

    public abstract void Attack();
    public void AttackPress() {
        attackButtonHold = true;
        System.Threading.Thread thread = new System.Threading.Thread(() => { while (attackButtonHold) Attack(); });
    }
    public void AttackRelease() {
        attackButtonHold = false;
    }
    protected abstract void InstallCardsFromChildren();
    public abstract bool InstallUnknownCard(Card card);
    public abstract bool UninstallUnknownCard(Card card);

    #endregion

    #region Service Methods

    protected void SetReliefTimer(float time) {
        // Create a timer with a two second interval.
        timer = new System.Timers.Timer(time * 1000);
        // Hook up the Elapsed event for the timer.
        timer.Elapsed += (sender, e) => { CanAttack = true; };
        timer.AutoReset = false;
        timer.Enabled = true;
    }
    protected void EnablePhysics() {
        rigidBody.bodyType = RigidbodyType2D.Dynamic; // "enabled" Rigidbody2D
        weaponCollider.enabled = true; // enabled Collider2D
    }
    protected void DisablePhysics() {
        rigidBody.bodyType = RigidbodyType2D.Kinematic; // "disabled" Rigidbody2D
        weaponCollider.enabled = false; // disabled Collider2D
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        this.transform.rotation = Quaternion.identity;
    }

    #endregion

    #region Overrided Methods

    protected void OnTriggerEnter2D(Collider2D collider) {
        if (state == State.Throwed && !collider.gameObject.Equals(friend)) {
            collider.gameObject.GetComponent<CharacterBase>()?.TakeDamage(rigidBody.velocity.normalized * throwHitDamage);
            DisablePhysics();
            Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
        }
    }

    #endregion

    #region Main Methods

    public void ChangeState() => WeaponState = (WeaponState == State.Alt) ? State.Main : State.Alt;

    // To-Do
    //private void Drop() {
    //    WeaponState = State.Throwed;

    //    this.gameObject.transform.parent = null; // unparented weapon
    //    Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
    //    Debug.Log(TAG + "Dropped weapon");
    //}

    public void Throw(GameObject whoThrowed, Vector2 direction) {
        WeaponState = State.Throwed;
        friend = whoThrowed;

        gameObject.transform.parent = null; // unparented weapon

        EnablePhysics();
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        rigidBody.velocity += direction; // "throwed" the weapon
        rigidBody.AddTorque(-Mathf.Sign(direction.x) * direction.magnitude * 150f);
        Debug.Log(rigidBody.angularVelocity);
    }

    #endregion

    #region Environment (Interaction)

    public override void Interact(GameObject whoInterracted) {
        // To-Do
        var cb = whoInterracted?.GetComponent<CharacterBase>();
        if (cb != null && cb.transform != transform.parent.parent) {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            DisablePhysics();
            gameObject.transform.parent = whoInterracted.GetComponentInChildren<Inventory>().transform;
            friend = null;
            WeaponState = State.Main;
            Debug.Log(TAG + "Picked");
        }
    }

    #endregion

    #region Inner Structures

    public enum State {
        Main,
        Alt,
        Throwed
    }

    #endregion
}
