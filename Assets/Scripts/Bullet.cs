using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool ricochet;
    public bool piercing;
    public bool homing;
    public bool teleporting;
    public bool magnet;
    public LayerMask whatIsAlive;
    public LayerMask enemy;
    public LayerMask magnetting;

    private GameObject aim;
    private int piercingCount;

    public Bullet(bool ricochet = false,
        bool piercing = false,
        bool homing = false,
        bool teleporting = false,
        bool magnet = false)
    {
        this.ricochet = ricochet;
        this.piercing = piercing;
        this.homing = homing;
        this.teleporting = teleporting;
        this.magnet = magnet;
    }

    private void Start()
    {
        if (ricochet)
        {
            PhysicsMaterial2D bulletPhMat = new PhysicsMaterial2D();
            bulletPhMat.bounciness = 1;
            this.GetComponent<BoxCollider2D>().sharedMaterial = bulletPhMat;
        }

        if (homing)
        {
            if(Physics2D.OverlapCircle(transform.position, 20, enemy) != null)
            aim = Physics2D.OverlapCircle(transform.position, 20, enemy).gameObject;
        }

        if (piercing)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg);


        if (homing && aim != null)
        {
            Vector2 dist = (aim.transform.position - this.transform.position).normalized * 20;
            Vector2 force = (dist - rb.velocity) * 2;
            rb.AddForce(force);
        }

        if (magnet)
        {
            Collider2D[] cols = new Collider2D[10];
            Physics2D.OverlapCircleNonAlloc(this.transform.position, 10, cols, magnetting);
            foreach(Collider2D col in cols)
            {
                if (col != null && col.GetComponent<Rigidbody2D>() != null)
                {
                    Vector2 dist = (this.transform.position - col.transform.position);
                    Vector2 force = (dist.normalized * 20 - col.GetComponent<Rigidbody2D>().velocity) * 2;
                    col.GetComponent<Rigidbody2D>().AddForce((force.normalized / Mathf.Pow(dist.magnitude, 2)) * 15);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ricochet)
        {
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        piercingCount--;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (piercingCount <= 0)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
