using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : CharacterBase
{

    public override float HP
    {
        get => _hp;
        protected set
        {
            _hp = value > MaxHP ? MaxHP : (value < 0 ? 0 : value);
            MainData.ActionHPChange?.Invoke();
        }
    }
    public override float SP
    {
        get => _sp;
        protected set
        {
            _sp = value > MaxSP ? MaxSP : (value < 0 ? 0 : value);
            MainData.ActionSPChange?.Invoke();
        }
    }
    public override float OP
    {
        get => _op;
        protected set
        {
            _op = value > MaxOP ? MaxOP : (value < 0 ? 0 : value);
            MainData.ActionOPChange?.Invoke();
        }
    }

    private void Awake()
    {
        // Setting Player to MainData
        MainData.PlayerObject = gameObject;
    }

    //private new void Start()
    //{
    //    CurrentEffects[CardEffect.EffectType.Fire] = new EffectControl(
    //        new CardEffect.NestedProps(
    //            CardEffect.EffectType.Fire,
    //            1,
    //            20,
    //            1
    //            ), this);
    //}

    private new void Update() 
    {
        base.Update();
        if (IsMoving)
            Move(MainData.Controls.Player.Move.ReadValue<Vector2>());
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        foreach (Transform tr in GroundCheckers)
        {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
        Gizmos.color = Color.green;
        foreach (Transform tr in RightSideCheckers)
        {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
        Gizmos.color = Color.yellow;
        foreach (Transform tr in LeftSideCheckers)
        {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(EnvironmentChecker.transform.position, 0.1f);
    }

}
