using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Inventory inv = null;
    [SerializeField]
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
    }

    void Update() {
        SpeedX = Input.GetAxis("Horizontal") * HorizontalSpeed;
        tryJump = Input.GetButton("Jump");

        
    }

    void FixedUpdate()
    {
        
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