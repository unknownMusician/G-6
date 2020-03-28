using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public RoomSpawner rSp;
    public Rigidbody2D rb;
    public Gun gun;
    public float speed;
    public float jump;

    private float HorizMove;
    private bool isJump;

    private void Start() {
        this.transform.position = rSp.getCurrentLocationAll();
    }

    void Update()
    {
        HorizMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            gun.Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            gun.ChangeState();
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
