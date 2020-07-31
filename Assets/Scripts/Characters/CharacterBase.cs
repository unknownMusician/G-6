using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> GroundCheckers;
    [SerializeField]
    protected List<Transform> LeftSideCheckers;
    [SerializeField]
    protected List<Transform> RightSideCheckers;
    [Space]
    [SerializeField]
    protected EnvironmentChecker EnvironmentChecker;

    #region Fields
    [Space]
    [Space]
    [SerializeField]
    protected bool CanFly;

    [SerializeField]
    protected float JumpForce;

    [SerializeField]
    protected float VerticalSpeed;

    [SerializeField]
    protected float HorizontalSpeed;

    [SerializeField]
    protected float InteractionRadius;

    [SerializeField]
    protected float ClimbingSpeed;

    [SerializeField]
    protected LayerMask ContactLayer;

    [SerializeField] 
    protected float FatiguePerFrame;

    [SerializeField] 
    protected float HorizontalBust;

    [SerializeField]
    protected float SPRegenerationPerFrame;


    protected Dictionary<Side, List<Transform>> Checkers;

    public Dictionary<CardEffect.EffectType, EffectControl> CurrentEffects;

    protected float _hp;
    protected float _sp;
    protected float _op;
    #endregion

    #region Properties

    #region MaxValues
    public float MaxHP { get; protected set; }
    public float MaxSP { get; protected set; }
    public float MaxOP { get; protected set; }
    // ? public float MaxMP { get; protected set; } // Mana Point
    #endregion

    #region CurrentValues
    public virtual float HP { get => _hp; protected set => _hp = value > MaxHP ? MaxHP : (value < 0 ? 0 : value); }
    public virtual float SP { get => _sp; protected set => _sp = value > MaxSP ? MaxSP : (value < 0 ? 0 : value); }
    public virtual float OP { get => _op; protected set => _op = value > MaxSP ? MaxSP : (value < 0 ? 0 : value); }
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

    protected BaseEnvironment InteractableObject => EnvironmentChecker.ClosestEnvironment;

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


    #region Weapons&Inventory

    #region Properties
    protected Inventory Inventory => GetComponentInChildren<Inventory>();
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
    protected void MoveX(float dir, float jump, bool run = false)
    {
        float horizontalSpeed = HorizontalSpeed;
        if (run && Math.Abs(dir) > 0)
        {
            SP -= FatiguePerFrame * Time.deltaTime;
            horizontalSpeed *= (Math.Abs(SP) > SPRegenerationPerFrame * Time.deltaTime * 2 ? HorizontalBust : 1);
        }
        switch (State)
        {
            case State.OnGround:
                rb.velocity = new Vector2(dir * horizontalSpeed, Math.Abs(jump) > 0 ? jump * JumpForce : rb.velocity.y);
                //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce : 0), ForceMode2D.Impulse);
                break;
            //case State.Climb:
            //    rb.velocity = new Vector2(dir * horizontalSpeed, jump ? JumpForce / 4 : rb.velocity.y);
            //    //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : 0), ForceMode2D.Impulse);
            //    break;
            case State.Swim:
                rb.velocity = new Vector2(dir * horizontalSpeed, Math.Abs(jump) > 0 ? jump * JumpForce / 4 : rb.velocity.y);
                //rb.AddForce(new Vector2(dir * HorizontalSpeed, jump ? JumpForce / 4 : 0), ForceMode2D.Impulse);
                break;
            case State.OnAir:
                rb.velocity = new Vector2(dir * horizontalSpeed, rb.velocity.y);
                //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
                break;
            default:
                rb.velocity = new Vector2(dir * horizontalSpeed, Math.Abs(jump) > 0 ? jump * JumpForce : rb.velocity.y);
                //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
                break;
        }
    }
    protected void MoveY(float dir, bool jump)
    {
        float verticalSpeed = VerticalSpeed;
        if (jump)
        {
            if (ClimbingBySide() == Side.Left)
                //rb.velocity = new Vector2(JumpForce / 2f, JumpForce);
                rb.AddForce(new Vector2(HorizontalSpeed , JumpForce * (Input.GetAxisRaw("Horizontal") < 0 ? -0.5f : 0.5f)), ForceMode2D.Impulse);
            else
                //rb.velocity = new Vector2(-JumpForce / 2f, JumpForce);
                rb.AddForce(new Vector2(-HorizontalSpeed, JumpForce * (Input.GetAxisRaw("Horizontal") > 0 ? -0.5f : 0.5f)), ForceMode2D.Impulse);
            State = State.OnAir;
        }
        else 
            rb.velocity = new Vector2(0, dir * verticalSpeed * (SP > 0 ? 1 : 0));
        //rb.AddForce(new Vector2(0, dir == 0 ? 0 : dir * ClimbingSpeed), ForceMode2D.Impulse);
        if(Math.Abs(dir) > 0)
            SP -= FatiguePerFrame * Time.deltaTime ;
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
        //try
        //{
        //    if (InteractableObject == null)
        //        return false;
        //    InteractableObject.Interact();
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    Logger.LogW(ex, "Problems with interraction in CharacterBase script");
        //}
        //return false;

        try
        {
            List<BaseEnvironment> allEnvironment = GameObject.FindObjectsOfType<BaseEnvironment>()
                .OrderBy(p => (this.transform.position - p.transform.position).sqrMagnitude)
                .ToList();

            if (allEnvironment.Count != 0)
            {
                if ((this.transform.position - allEnvironment[0].transform.position).sqrMagnitude < InteractionRadius)
                {
                    allEnvironment[0].Interact(this.gameObject);
                    Debug.DrawLine(this.transform.position, allEnvironment[0].transform.position);
                    return true;
                }
            }

        }
        catch (Exception ex)
        {
            Logger.LogW(ex, "Problems with interaction in CharacterBase script");
        }

        return false;
    }

    protected void EffectsFixedControl()
    {
        foreach (CardEffect.EffectType effect in Enum.GetValues(typeof(CardEffect.EffectType)).Cast<CardEffect.EffectType>())
        {
            if (CurrentEffects.ContainsKey(effect))
                CurrentEffects[effect].Act(Time.deltaTime);
        }
    }
    protected void SPFixedControl()
    {
        if (this.State == State.OnGround)
        {
            float sp = SP + SPRegenerationPerFrame * Time.deltaTime;
            SP = sp > MaxSP ? MaxSP : sp;
        }
    }

    protected void Die()
    {
        State = State.Dead;
        Say("Hm, bye. I'm dead");
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

        CurrentEffects = new Dictionary<CardEffect.EffectType, EffectControl>();

        HP = MaxHP;
        SP = MaxSP;
        OP = MaxOP;

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
        EffectsFixedControl();
        SPFixedControl();
    }
    #endregion


    #region Public Methods

    public void TakeDamage(float damage)
    {
        HP -= damage;
        Say($"Ouch, I've taken {damage} damage at {DateTime.Now: hh:mm:ss t z}. Now I have {HP} HP");
        if (HP <= 0)
            Die();
    }
    public void TakeDamage(Vector2 damageVector)
    {
        rb.AddForce(damageVector / 10f, ForceMode2D.Impulse);
        TakeDamage(damageVector.magnitude);
    }
    public void TakeDamage(Vector2 damageVector, CardEffect.NestedProps prop)
    {
        rb.AddForce(damageVector / 10f, ForceMode2D.Impulse);
        TakeDamage(damageVector.magnitude);
        TakeEffect(prop);
    }

    public void TakeEffect(CardEffect.NestedProps prop)
    {
        if (CurrentEffects.ContainsKey(prop.Effect))
        {
            CurrentEffects[prop.Effect].ChangeParams(prop);
        }
        else
        {
            CurrentEffects[prop.Effect] = new EffectControl(prop, this);
        }
    }
    #endregion

}
