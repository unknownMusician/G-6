using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    private Inventory inv = null;
    [SerializeField]
    private RoomSpawner rSp = null;
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

    void Update() {
        SpeedX = Input.GetAxis("Horizontal") * HorizontalSpeed;
        tryJump = Input.GetButton("Jump");

        #region Guns
        #region Changing weapon
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
        #endregion
        if (inv.Weapon != null) {
            #region Changing weapon state
            if (Input.GetButtonDown("Fire2")) {
                inv.Weapon.ChangeState();
            }
            #endregion
            #region Reloading
            if (Input.GetButtonDown("Reload")) {
                inv.Weapon.Reload();
            }
            #endregion
        }
        #endregion
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

        #region Guns
        if (inv.Weapon != null) {
            #region Shoot
            if (Input.GetButton("Fire1"))
                inv.Weapon.Attack();
            #endregion
        }
        #region Throw
        if (Input.GetButtonDown("Throw"))
            inv.ThrowPress();
        if (Input.GetButtonUp("Throw"))
            inv.ThrowRelease();
        #endregion
        #endregion
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