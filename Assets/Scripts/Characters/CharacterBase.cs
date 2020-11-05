using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InteractableChecker))]
public abstract class CharacterBase : MonoBehaviour
{
    #region Fields
    [Header("Checkers")]
    [Header("-------------------- CharacterBase --------------------")]
    [SerializeField]
    protected List<Vector2> GroundCheckers;
    [SerializeField]
    protected List<Vector2> LeftSideCheckers;
    [SerializeField]
    protected List<Vector2> RightSideCheckers;

    [Header("Values")]
    [SerializeField] protected bool CanFly = false;
    [SerializeField] protected Vector2 JumpForce = new Vector2(120, 200);
    [SerializeField] protected Vector2 Speed = new Vector2(15, 15);
    [SerializeField] [Range(0, .3f)] protected float AccelerationTime = 0.05f;
    [SerializeField] protected float InteractionRadius = 30;
    [SerializeField] protected float ClimbingSpeed = 10;
    [SerializeField] protected LayerMask ContactLayer = 0;
    [SerializeField] protected float SpPerFrameDecrease = 40;
    [SerializeField] protected float SpPerFrameIncrease = 10;
    [SerializeField] protected float HorizontalRunBoost = 1.5f;
    [SerializeField] protected float HorizontalSneakBoost = 0.5f;

    protected float GravityMultiplier = 1f;
    protected Vector2 _currAccel = Vector2.zero;

    protected Dictionary<Side, List<Vector2>> Checkers;

    public Dictionary<CardEffect.EffectType, EffectControl> CurrentEffects;

    protected float _hp;
    protected float _sp;
    protected float _op;

    [Header("MaxValues")]
    [SerializeField] protected float _hpMax = 100;
    [SerializeField] protected float _spMax = 100;
    [SerializeField] protected float _opMax = 100;
    #endregion

    #region Properties

    #region Input

    public bool IsRunning { get; set; }
    public bool IsCrouching { get; set; }
    public bool IsMoving { get; set; }

    #endregion

    #region MaxValues
    public float MaxHP { get => _hpMax; protected set => _hpMax = value; }
    public float MaxSP { get => _hpMax; protected set => _spMax = value; }
    public float MaxOP { get => _hpMax; protected set => _opMax = value; }
    #endregion

    #region CurrentValues
    public virtual float HP { get => _hp; protected set => _hp = value > MaxHP ? MaxHP : (value < 0 ? 0 : value); }
    public virtual float SP { get => _sp; protected set => _sp = value > MaxSP ? MaxSP : (value < 0 ? 0 : value); }
    public virtual float OP { get => _op; protected set => _op = value > MaxSP ? MaxSP : (value < 0 ? 0 : value); }
    #endregion

    #region Other Public Props
    public short Level { get; protected set; }
    public State State { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    public Fraction Fraction { get; protected set; }
    public Inventory Inventory { get; protected set; }

    #endregion

    #endregion

    #region Environment

    protected InteractableBase InteractableObject => InteractableChecker.ClosestEnvironment;

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

    protected InteractableChecker InteractableChecker;

    protected Collider2D crouchDisabledCollider; // todo: crouching Physics & anim

    #endregion

    #region Common Methods

    public void Move(Vector2 dir, bool run = true, bool sneak = true)
    {
        dir = MainData.SquareNormalized(dir);
        Vector2 finSpeed = new Vector2(0, rb.velocity.y);
        float deltaSP = SpPerFrameDecrease * Time.deltaTime;
        // X
        if (State != State.Climb && dir.x != 0) 
        {
            run &= IsRunning;
            sneak &= IsCrouching;
            float horizontalSpeed = Speed.x;
            // running
            if (run) {
                SP -= deltaSP;
                horizontalSpeed *= (SP > deltaSP ? HorizontalRunBoost : 1);
            }
            // sneaking
            else if (sneak) { horizontalSpeed *= HorizontalSneakBoost; }
            // fin X
            finSpeed.x = horizontalSpeed * dir.x;
        }
        // Y
        if (State == State.Climb && dir.y != 0)
        {
            // if climbing down
            if (dir.y > 0) { SP -= deltaSP; }
            // fin Y
            if (SP > deltaSP || dir.y < 0) { finSpeed.y = dir.y * Speed.y; }
        }

        // fin
        rb.velocity = Vector2.SmoothDamp(rb.velocity, finSpeed, ref _currAccel, AccelerationTime);
    }
    public void Jump()
    {
        switch (State)
        {
            case State.Climb:
                rb.AddForce(
                    new Vector2((ClimbingBySide() == Side.Left ? 1 : -1) * JumpForce.x, JumpForce.y * 0.5f),
                    ForceMode2D.Impulse);
                return;

            case State.Swim:
                JumpForce /= 4f;
                break;

            case State.OnAir:
                if (!CanFly) { return; }
                break;
        }

        rb.AddForce(new Vector2(0, JumpForce.y), ForceMode2D.Impulse);
    }

    protected bool isOnLayer(string layer, Side side)
    {
        foreach (var checker in Checkers[side])
            if (Physics2D.Linecast(transform.position, (Vector2)transform.position + checker, 1 << LayerMask.NameToLayer(layer)))
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
    public void CheckSideLR(Vector3 localTriger, bool inverse = false)
    {
        if (localTriger.x > 0)
            sr.flipX = inverse;
        else
            sr.flipX = !inverse;
    }
    protected float CheckGravityBeState()
    {
        switch (State)
        {
            case State.OnGround:
                GravityMultiplier = 1f;
                break;
            case State.Climb:
                GravityMultiplier = 1f;
                break;
            case State.Swim:
                GravityMultiplier = 0.5f;
                break;
            case State.OnAir:
                GravityMultiplier = 1f;
                break;
            default:
                GravityMultiplier = 1f;
                break;
        }
        return MainData.Constants.gravityScale * GravityMultiplier;
    }
    protected Side ClimbingBySide()
    {
        if (isOnLayer("Ground", Side.Left))
            return Side.Left;
        if (isOnLayer("Ground", Side.Right))
            return Side.Right;
        return Side.Up;
    }


    public bool TryInteract()
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

        //try
        //{
            List<InteractableBase> allEnvironment = GameObject.FindObjectsOfType<InteractableBase>()
                .OrderBy(p => (this.transform.position - p.transform.position).sqrMagnitude)
                .ToList();

        if (allEnvironment.Count != 0)
        {
            for (int i = 0; i < allEnvironment.Count; i++) {
                if ((this.transform.position - allEnvironment[i].transform.position).sqrMagnitude < InteractionRadius) {
                    bool result = allEnvironment[i].Interact(this.gameObject);
                    if(result)
                    {
                        Debug.DrawLine(this.transform.position, allEnvironment[0].transform.position);
                        return true;
                    }
                }
            }
        }

        //}
        //catch (Exception ex)
        //{
        //    Logger.LogW(ex, "Problems with interaction in CharacterBase script");
        //}

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
            float sp = SP + SpPerFrameIncrease * Time.deltaTime;
            SP = sp > MaxSP ? MaxSP : sp;
        }
    }
    protected void Gravity() {
        float gravityScale = MainData.Constants.gravityScale * GravityMultiplier * rb.mass * 10;
        if (CheckState() == State.Climb) {
            switch (ClimbingBySide()) {
                case Side.Left:
                    rb.AddForce(Vector2.left * gravityScale, ForceMode2D.Force);
                    return;
                case Side.Right:
                    rb.AddForce(Vector2.right * gravityScale, ForceMode2D.Force);
                    return;
            }
        }
        rb.AddForce(Vector2.down * gravityScale, ForceMode2D.Force);
        // todo
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
        //Debug.Log($"{name} said: \"{message}\" ");
    }

    #endregion

    #region MonoBehaviour Implemented
    protected void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Inventory = GetComponentInChildren<Inventory>();
        InteractableChecker = GetComponent<InteractableChecker>();

        Checkers = new Dictionary<Side, List<Vector2>> {
            { Side.Down, GroundCheckers },
            { Side.Left, LeftSideCheckers },
            { Side.Right, RightSideCheckers }
        };

        CurrentEffects = new Dictionary<CardEffect.EffectType, EffectControl>();

        HP = MaxHP;
        SP = MaxSP;
        OP = MaxOP;

        State = CheckState();
    }
    protected void Update()
    {
        if (!Pause.GameIsPaused && State != State.Dead) {
            CheckGravityBeState();
            State = CheckState();
        }
    }
    protected void FixedUpdate()
    {
        EffectsFixedControl();
        SPFixedControl();

        // todo
        Gravity();
    }
    #endregion


    #region Public Methods

    public void TakeDamage(float damage)
    {
        HP -= damage;
        Say($"Ouch, I've taken {damage} damage. Now I have {HP} HP");
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

    protected void OnDrawGizmos() {
        Gizmos.color = Color.grey;
        foreach (var v in GroundCheckers) {
            Gizmos.DrawSphere((Vector2)transform.position + v, 0.1f);
        }
        Gizmos.color = Color.green;
        foreach (var v in RightSideCheckers) {
            Gizmos.DrawSphere((Vector2)transform.position + v, 0.1f);
        }
        Gizmos.color = Color.yellow;
        foreach (var v in LeftSideCheckers) {
            Gizmos.DrawSphere((Vector2)transform.position + v, 0.1f);
        }
    }
}