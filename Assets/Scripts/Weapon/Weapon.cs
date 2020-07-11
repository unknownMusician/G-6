using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;

public abstract class Weapon : MonoBehaviour {

    const string TAG = "Weapon: ";

    #region Actions
    public Action OnAttackAction;
    public Action OnInstallCardAction;
    #endregion

    #region Public Variables
    [SerializeField]
    private GameObject weaponPrefab;

    public GameObject WeaponPrefab { get { return weaponPrefab; } }

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

    #endregion

    #region Constants

    public enum State {
        Main = 0,
        Alt = 1,
        Throwed = 2
    }

    #endregion

    #region Abstract Methods

    public abstract void Attack();
    protected abstract void InstallModCards();
    protected abstract void GetCardsFromChildren();
    public abstract List<GameObject> GetAllCardsList();

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
        weaponCollider.enabled = false; // disabled Collider2D
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        this.transform.rotation = Quaternion.identity;
    }

    #endregion

    #region Overrided Methods

    protected void OnTriggerEnter2D(Collider2D collider) {
        if (state == State.Throwed && !collider.gameObject.Equals(friend)) {
            CharacterBase cb = collider.gameObject.GetComponent<CharacterBase>();
            if (cb != null) {
                cb.TakeDamage(rigidBody.velocity.normalized * throwHitDamage);
            }
            DisablePhysics();
            Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
        }
    }

    #endregion

    #region Main Methods

    public void ChangeState() {
        if (state == State.Alt) {
            state = State.Main;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("secondState", false);
            //weaponCollider.enabled = false;

        } else {
            state = State.Alt;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            animator.SetBool("secondState", true);
            //weaponCollider.enabled = true;
        }
        Debug.Log(TAG + "Changed state");
    }
    private void Drop() {
        state = State.Throwed;

        this.gameObject.transform.parent = null; // unparented weapon
        Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
        Debug.Log(TAG + "Dropped weapon");
    }
    public void Throw(GameObject whoThrowed, Vector2 direction) {
        state = State.Throwed;
        friend = whoThrowed;

        this.gameObject.transform.parent = null; // unparented weapon
        EnablePhysics();
        rigidBody.velocity += direction; // "throwed" the weapon
        rigidBody.angularVelocity = -direction.magnitude * 150f;
    }

    #endregion

    #region Inner Classes

    public class Info {
        public GameObject WeaponPrefab;
        public List<GameObject> CardPrefabs;

        public Info(GameObject weaponPrefab, List<GameObject> cardPrefabs) {
            this.WeaponPrefab = weaponPrefab;
            this.CardPrefabs = cardPrefabs;
        }
    }

    #endregion
}
