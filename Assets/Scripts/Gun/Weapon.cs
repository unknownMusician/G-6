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
    [SerializeField]
    protected List<GunModule> modules = null;


    [SerializeField]
    protected List<Sprite> spriteArray = null;
    protected Dictionary<int, Sprite> sprites;

    [SerializeField]
    private float hitStrenght = 10;

    protected Timer timer;

    protected bool isThrowed = false;
    protected bool secondState = false;
    protected bool canAttack = true;

    public abstract void Attack();
    protected abstract void SetSprite();
    protected abstract void InstallMods();
    protected abstract void GetModulesFromChildren();
    public abstract void Reload();
    protected void FillDictionary() {
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
    protected void OnTriggerEnter2D(Collider2D collision) {
        Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (crb != null) {
            Vector2 dist = collision.transform.position - this.transform.position;
            crb.velocity += dist.normalized * hitStrenght;
        }
        DisablePhysics();
        Instantiate(weaponHolder, this.gameObject.transform.position, Quaternion.identity).GetComponent<WeaponHolder>().SetChild(this.transform);
    }
}
