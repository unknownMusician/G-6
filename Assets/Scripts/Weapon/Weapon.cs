using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public abstract class Weapon : MonoBehaviour {

    readonly string TAG = "Weapon: ";

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
    protected float hitStrenght = 10;

    #endregion

    #region Private Variables

    protected List<Card> cards;

    protected Timer timer;

    protected bool isThrowed = false;
    protected bool secondState = false;
    protected bool canAttack = true;

    #endregion

    #region Abstract Methods

    public abstract void Attack();
    protected abstract void InstallModCards();
    protected abstract void GetCardsFromChildren();
    public abstract void Reload();

    #endregion

    #region Getters

    public Collider2D GetWeaponCollider() {
        return weaponCollider;
    }

    #endregion

    #region Service Methods

    protected void SetReliefTimer(float time) {
        // Create a timer with a two second interval.
        timer = new System.Timers.Timer(time * 1000);
        // Hook up the Elapsed event for the timer. 
        timer.Elapsed += SetCanAttack;
        timer.AutoReset = false;
        timer.Enabled = true;
    }
    protected void SetCanAttack(object sender, ElapsedEventArgs e) {
        canAttack = true;
    }
    public void EnablePhysics() {
        rigidBody.bodyType = RigidbodyType2D.Dynamic; // "enabled" Rigidbody2D
        weaponCollider.enabled = true; // enabled Collider2D
    }
    public void DisablePhysics() {
        rigidBody.bodyType = RigidbodyType2D.Kinematic; // "disabled" Rigidbody2D
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        this.transform.rotation = Quaternion.identity;
        weaponCollider.enabled = false; // disabled Collider2D
    }

    #endregion

    #region Overrided Methods

    protected void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (crb != null) {
            Vector2 dist = collision.transform.position - this.transform.position;
            crb.velocity += dist.normalized * hitStrenght;
        }
        DisablePhysics();
        Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
    }

    #endregion

    #region Main Methods

    public void ChangeState() {
        if (secondState) {
            secondState = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("secondState", false);
            //weaponCollider.enabled = false;

        } else {
            secondState = true;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            animator.SetBool("secondState", true);
            //weaponCollider.enabled = true;
        }
        Debug.Log(TAG + "Changed state");
    }
    private void Drop() {
        isThrowed = true;

        this.gameObject.transform.parent = null; // unparented weapon
        EnablePhysics();
        Debug.Log(TAG + "Dropped weapon");
    }
    public void Throw(Vector2 direction) {
        isThrowed = true;

        this.gameObject.transform.parent = null; // unparented weapon
        EnablePhysics();
        rigidBody.velocity += direction; // "throwed" the weapon
        rigidBody.angularVelocity = -direction.magnitude * 30;
    }

    #endregion
}
