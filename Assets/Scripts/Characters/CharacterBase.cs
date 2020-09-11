﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> GroundCheckers;
    [SerializeField]
    protected List<Transform> LeftSideCheckers;
    [SerializeField]
    protected List<Transform> RightSideCheckers;
    [SerializeField, Space]
    protected EnvironmentChecker EnvironmentChecker;

    #region Fields

    [Space, Space]

    [SerializeField]
    protected bool CanFly; // todo: unused;

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
    protected float HorizontalBoost;

    [SerializeField]
    protected float HorizontalAntiBoost;

    [SerializeField]
    protected float SPRegenerationPerFrame;


    protected Dictionary<Side, List<Transform>> Checkers;

    public Dictionary<CardEffect.EffectType, EffectControl> CurrentEffects;

    protected float _hp;
    protected float _sp;
    protected float _op;

    [Space, Space]
    [SerializeField] protected float _hpMax = 100;
    [SerializeField] protected float _spMax = 100;
    [SerializeField] protected float _opMax = 100;
    #endregion

    #region Properties

    #region Input

    public bool IsRunning { get; set; }
    public bool IsSneaking { get; set; }
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
    public Side Side { get; set; }
    public short Level { get; protected set; }
    public State State { get; protected set; }
    public Bonuses Bonuses { get; protected set; }
    public Fraction Fraction { get; protected set; }
    public Inventory Inventory { get; protected set; }

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

    #region Common Methods
    protected void TurnToRightSide(bool inverse = false)
    {
        if (Side == Side.Left)
            sr.flipX = !inverse;
        else
            sr.flipX = inverse;
    }

    public void Move(Vector2 dir, bool run = true, bool sneak = true)
    {
        _goDir = dir;
        _goRun = run;
        _goSneak = sneak;
    }
    private Vector2 _goDir = Vector2.zero;
    private bool _goRun = true;
    private bool _goSneak = true;
    private void Go() // todo: something wrong - something in this function refuses to jump;
    {
        Vector2 finMove = rb.velocity;
        // X
        if (State != State.Climb && _goDir.x != 0)
        {
            _goRun &= IsRunning;
            _goSneak &= IsSneaking;
            float horizontalSpeed = HorizontalSpeed;
            // running
            if (_goRun && Math.Abs(_goDir.x) > 0)
            {
                SP -= FatiguePerFrame * Time.deltaTime;
                horizontalSpeed *= (Math.Abs(SP) > SPRegenerationPerFrame * Time.deltaTime * 2 ? HorizontalBoost : 1);
            }
            // sneaking
            else if (_goSneak && Math.Abs(_goDir.x) > 0)
                horizontalSpeed *= HorizontalAntiBoost;

            finMove.x = horizontalSpeed * _goDir.x;

        }
        // Y
        if (State == State.Climb) {
            finMove.y = _goDir.y * VerticalSpeed * (SP > 0 ? 1f : 0f);

            if (_goDir.y != 0)
                SP -= FatiguePerFrame * Time.deltaTime;
        }

        // fin
        rb.velocity = finMove;
        //revert
        _goDir = Vector2.zero;
        _goRun = true;
        _goSneak = true;
    }
    public void Jump()
    {
        float jumpForce = JumpForce;

        switch (State)
        {
            case State.Climb:
                rb.AddForce(
                    ClimbingBySide() == Side.Left
                        ? new Vector2(HorizontalSpeed, JumpForce * (MainData.Controls.Player.Move.ReadValue<Vector2>().x < 0 ? -0.5f : 0.5f))
                        : new Vector2(-HorizontalSpeed, JumpForce * (MainData.Controls.Player.Move.ReadValue<Vector2>().x < 0 ? -0.5f : 0.5f)),
                    ForceMode2D.Impulse);
                return; // todo: исправить прыжки от стен;

            case State.Swim:
                jumpForce /= 4f;
                break;

            case State.OnAir:
                if(!CanFly)
                    return;
                break;
        }

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
    public Side CheckSideLR(Vector3 worldTriger)
    {
        if (worldTriger.x > transform.position.x)
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
        Debug.Log($"{name} said: \"{message}\" ");
    }

    #endregion

    #region MonoBehaviour Implemented
    protected void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Inventory = GetComponentInChildren<Inventory>();

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
        if (!Pause.GameIsPaused && State != State.Dead) {
            State = CheckState();
            TurnToRightSide();
            CheckGravityBeState();
        }
    }
    protected void FixedUpdate()
    {
        EffectsFixedControl();
        SPFixedControl();

        Go();
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

}
