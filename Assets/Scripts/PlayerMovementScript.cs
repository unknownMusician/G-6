//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class PlayerMovementScript : MonoBehaviour
//{
//    public RoomSpawner rSp;
//    public Rigidbody2D rb;
//    public Gun gun;
//    public float speed;
//    public float jump;

//    private float HorizMove;
//    private bool isJump;

//    private void Start() {
//        this.transform.position = rSp.getCurrentLocationAll();
//    }

//    void Update()
//    {
//        HorizMove = Input.GetAxis("Horizontal");
//        if (Input.GetButtonDown("Jump"))
//        {
//            isJump = true;
//        }

//        if (Input.GetButtonDown("Fire1"))
//        {
//            gun.Attack();
//        }

//        if (Input.GetButtonDown("Fire2"))
//        {
//            gun.ChangeState();
//        }
//    }

//    private void FixedUpdate()
//    {
//        if (isJump)
//        {
//            rb.velocity = new Vector2(HorizMove * speed, jump);
//            isJump = false;
//        }
//        rb.velocity = new Vector2(HorizMove * speed, rb.velocity.y);
//    }
//}
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
        this.transform.position = rSp.getCurrentLocationAll();
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