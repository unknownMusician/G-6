using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Gun Gun;
    [SerializeField]
    private RoomSpawner rSp;
    private Rigidbody2D rb;
    private float SpeedX;
    private bool isGrounded = false;
    private bool tryJump = false;
    private bool isOnWall = false;

    public float HorizontalSpeed;
    public float VerticalImpulce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rSp != null) this.transform.position = rSp.getCurrentLocationAll();
    }

    void Update()
    {
        SpeedX = Input.GetAxis("Horizontal") * HorizontalSpeed;
        tryJump = Input.GetButton("Jump");

    }

    void FixedUpdate()
    {
        if (isOnWall)
        {
            rb.velocity = new Vector2(SpeedX, tryJump ? VerticalImpulce / 4 : 0);
        }
        else
        {
            rb.velocity = new Vector2(SpeedX, tryJump && isGrounded ? VerticalImpulce : rb.velocity.y);
        }

        if (Input.GetButton("Fire1"))
            Gun.Shoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Ground": isGrounded = true; break;
            case "VerticalWall": isOnWall = true; break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Ground": isGrounded = false; break;
            case "VerticalWall": isOnWall = false; break;
        }
    }
}