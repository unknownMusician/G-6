using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public Gun gun;
    public float speed;
    public float jump;

    private float HorizMove;
    private bool isJump;

    void Update()
    {
        HorizMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }

        if (Input.GetButton("Fire1"))
        {
            gun.Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(HorizMove * speed, jump);
            isJump = false;
        }
        rb.velocity = new Vector2(HorizMove * speed, rb.velocity.y);
    }
}
