using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public abstract class Weapon : MonoBehaviour {

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

    protected List<Card> cards;

    [SerializeField]
    protected float hitStrenght = 10;

    protected Timer timer;

    protected bool isThrowed = false;
    protected bool secondState = false;
    protected bool canAttack = true;

    public abstract void Attack();
    protected abstract void InstallModCards();
    protected abstract void GetModulesFromChildren();
    public abstract void Reload();
    public Collider2D GetWeaponCollider() {
        return weaponCollider;
    }
    public void ChangeState() {
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
    private void Drop() {
        isThrowed = true;

        this.gameObject.transform.parent = null; // unparented weapon
        EnablePhysics();
    }
    public void Throw(Vector2 direction) {
        isThrowed = true;

        this.gameObject.transform.parent = null; // unparented weapon
        EnablePhysics();
        rigidBody.velocity += direction; // "throwed" the weapon
        rigidBody.angularVelocity = -direction.magnitude * 30;
    }
    protected void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (crb != null) {
            Vector2 dist = collision.transform.position - this.transform.position;
            crb.velocity += dist.normalized * hitStrenght;
        }
        DisablePhysics();
        Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
    }
}
