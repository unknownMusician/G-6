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

    void Update()
    {
        SpeedX = Input.GetAxis("Horizontal") * HorizontalSpeed;
        tryJump = Input.GetButton("Jump");
        { // start
            if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                inv.ChooseNext();
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                inv.ChoosePrev();
            }
            if (Input.GetButtonDown("WeaponSlot1")) {
                inv.Choose(0);
            } else if (Input.GetButtonDown("WeaponSlot2")) {
                inv.Choose(1);
            } else if (Input.GetButtonDown("WeaponSlot3")) {
                inv.Choose(2);
            } else if (Input.GetButtonDown("WeaponSlot4")) {
                inv.Choose(3);
            }
            // end
        } // changing weapon
        {
            if (Input.GetButtonDown("Fire2")) {
                inv.ChangeState();
            }
        } // changing weapon state
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
            inv.Attack();
        if (Input.GetButton("Throw"))
            inv.Throw();
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