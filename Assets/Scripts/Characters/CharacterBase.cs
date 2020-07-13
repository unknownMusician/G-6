using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> GroundCheckers;
    [SerializeField]
    protected List<Transform> LeftSideCheckers;
    [SerializeField]
    protected List<Transform> RightSideCheckers;

    #region Fields
    [SerializeField]
    protected bool CanFly;

    [SerializeField]
    protected float JumpForce;

    [SerializeField]
    protected float HorizontalSpeed;

    [SerializeField]
    protected float ClimbingSpeed;

    [SerializeField]
    protected LayerMask ContactLayer;

    [SerializeField]
    protected Dictionary<Side, List<Transform>> Checkers;
    #endregion

    #region Properties

    #region MaxValues
    public float MaxHP { get; protected set; }
    public float MaxSP { get; protected set; }
    public float MaxOP { get; protected set; }
    // ? public float MaxMP { get; protected set; } // Mana Point
    #endregion

    #region CurrentValues
    public float HP { get; protected set; }
    public float SP { get; protected set; }
    public float OP { get; protected set; }
    // ? public float MP { get; protected set; } // Mana Point
    #endregion

    #region Other Public Props
    public Side Side { get; protected set; }
    public short Level { get; protected set; }
    public State State { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    public Fraction Fraction { get; protected set; }
    #endregion

    #endregion

    #region Environment

    protected BaseEnvironment InteractableObject;

    protected float DistanceToInteractableObject
    {
        get
        {
            if (InteractableObject != null)
                return (InteractableObject.transform.position - this.transform.position).magnitude;
            else
                return float.MaxValue;
        }
    }

    #endregion

    #region Components

    protected Rigidbody2D rb;

    protected SpriteRenderer sr;

    #endregion  


    #region Guns&Inventory

    #region Fields
    [SerializeField]
    protected Inventory Inventory;
    #endregion

    #region Methods

    protected abstract void WeaponControl();
    protected abstract void WeaponFixedControl();

    #endregion

    #endregion


    #region Common Methods
    protected void TurnToRightSide(bool inverse = false)
    {
        if (Side == Side.Left)
            sr.flipX = !inverse;
        else
            sr.flipX = inverse;

        //if (Side == Side.Left)
        //    transform.rotation = new Quaternion(transform.rotation.x, Mathf.PI, transform.rotation.y, transform.rotation.w);
        //else
        //    transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.y, transform.rotation.w);
    }
    protected void MoveX(float dir, bool jump)
    {
        switch (State)
        {
            case State.OnGround:
                rb.velocity = new Vector2(dir * HorizontalSpeed, jump ? JumpForce : rb.velocity.y);
                //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce : 0), ForceMode2D.Impulse);
                break;
            case State.Climb:
                rb.velocity = new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : rb.velocity.y);
                //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : 0), ForceMode2D.Impulse);
                break;
            case State.Swim:
                rb.velocity = new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : rb.velocity.y);
                //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : 0), ForceMode2D.Impulse);
                break;
            case State.OnAir:
                rb.velocity = new Vector2(dir * HorizontalSpeed, rb.velocity.y);
                //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
                break;
            default:
                rb.velocity = new Vector2(dir * HorizontalSpeed, jump ? JumpForce : rb.velocity.y);
                //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
                break;
        }
    }
    protected void MoveY(float dir, bool jump)
    {
        if (jump)
        {
            if (ClimbingBySide() == Side.Left)
                rb.AddForce(new Vector2(JumpForce / 2f, JumpForce), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(-JumpForce / 2f, JumpForce), ForceMode2D.Impulse);
            State = State.OnAir;
            return;
        }
        rb.velocity = new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed);
        //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
    }

    protected bool isOnLayer(string layer, Side side)
    {
        foreach (var checker in Checkers[side])
            if (Physics2D.Linecast(transform.position, checker.position, 1 << LayerMask.NameToLayer(layer)))
                return true;
        return false;
    }
    protected State CheckState()
    {
        if (HP <= 0f)
            return State.Dead;

        if (isOnLayer("Ground", Side.Down))
            return State.OnGround;

        if (isOnLayer("Water", Side.Down))
            return State.Swim;

        if (isOnLayer("Ground", Side.Right) || isOnLayer("Ground", Side.Left))
            return State.Climb;

        return State.OnAir;
    }
    protected Side CheckSideLR(Vector3 triger)
    {
        if (triger.x > transform.position.x)
            return Side.Right;
        return Side.Left;
    }
    protected float CheckGravityBeState()
    {
        switch (State)
        {
            case State.OnGround:
                rb.gravityScale = 9.8f;
                break;
            case State.Climb:
                rb.gravityScale = 0f;
                break;
            case State.Swim:
                rb.gravityScale = 4.9f;
                break;
            case State.OnAir:
                rb.gravityScale = 9.8f;
                break;
            default:
                rb.gravityScale = 9.8f;
                break;
        }
        return rb.gravityScale;
    }
    protected Side ClimbingBySide()
    {
        if (isOnLayer("Ground", Side.Left))
            return Side.Left;
        if (isOnLayer("Ground", Side.Right))
            return Side.Right;
        return Side.Up;
    }

    protected bool TryInteract()
    {
        try
        {
            if (InteractableObject == null)
                return false;
            InteractableObject.Interact();
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogW(ex, "Problems with interraction in CharacterBase script");
        }
        return false;
    }

    protected void Die()
    {
        State = State.Dead;
    }
    #endregion

    #region _

    protected void Say(string message)
    {
        Debug.Log($"Hero sad: \"{message}\" ");
    }

    #endregion

    #region MonoBehaviour Implemented
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Checkers = new Dictionary<Side, List<Transform>>();
        Checkers.Add(Side.Down, GroundCheckers);
        Checkers.Add(Side.Left, LeftSideCheckers);
        Checkers.Add(Side.Right, RightSideCheckers);

        State = CheckState();
    }
    protected void Update()
    {
        State = CheckState();
        TurnToRightSide();
        CheckGravityBeState();
        WeaponControl();
    }
    protected void FixedUpdate()
    {
        WeaponFixedControl();
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
         GameObject collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<BaseEnvironment>() != null)
            if (collisionObject.gameObject.transform.position.magnitude < DistanceToInteractableObject)
                InteractableObject = collisionObject.GetComponent<BaseEnvironment>();
    }
    protected void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<BaseEnvironment>() == InteractableObject)
            InteractableObject = null;
    }
    #endregion


    #region Public Methods

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
            Die();
    }
    public void TakeDamage(Vector2 damageVector)
    {
        rb.AddForce(damageVector / 10f, ForceMode2D.Impulse);
        TakeDamage(damageVector.magnitude);
    }
    public void TakeDamage(Vector2 damageVector, CardEffect.NestedProps prop) {
        rb.AddForce(damageVector / 10f, ForceMode2D.Impulse);
        TakeDamage(damageVector.magnitude);
    }

    #endregion

}
