using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase
{
    private void Awake()
    {
        MainData.Player = this.gameObject;
    }
    private new void Start()
    {

        MaxHP = 100f;
        MaxSP = 100f;
        MaxOP = 100f;

        HP = MaxHP;
        SP = MaxSP;
        OP = MaxOP;

        base.Start();

        //CurrentEffects[CardEffect.EffectType.Fire] = new EffectControl(
        //    new CardEffect.NestedProps(
        //        CardEffect.EffectType.Fire,
        //        1,
        //        20,
        //        1
        //        ), this );
    }

    protected new void Update()
    {
        if (State != State.Dead)
        {
            base.Update();

            Side = CheckSideLR(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetButtonDown("Interact"))
            {
                if (TryInteract())
                {
                    Say("Ok, I've interacted with something. What's next?");
                }
                else
                {
                    Say("Hey, there is nothing to interact with!");
                }
            }

            Control();
        }
    }

    protected void Control()
    {
        if (State == State.OnAir)
        {
            if (State != State.Climb)
            {
                if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump")))
                    MoveX(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));
            }
            else
            {
                if ((Input.GetButton("Vertical") || Input.GetButtonDown("Jump")))
                    MoveY(Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
            }
        }
        else
        {
            if (State != State.Climb)
            {
                //if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump")))
                MoveX(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"));
            }
            else
            {
                //if ((Input.GetButton("Vertical") || Input.GetButtonDown("Jump")))
                MoveY(Input.GetAxis("Vertical"), Input.GetButtonDown("Jump"));
            }
        }
    }

    #region WeaponControl

    private bool weaponChooseNext = false;
    private bool weaponChoosePrev = false;
    private bool weaponChooseFirst = false;
    private bool weaponChooseSecond = false;
    private bool weaponChooseThird = false;
    private bool weaponChooseFourth = false;
    private bool weaponChangeState = false;
    private bool weaponReload = false;
    private bool weaponAttack = false;
    private bool weaponThrowPress = false;
    private bool weaponThrowRelease = false;
    private Vector3 weaponAimPoint = Vector3.right;

    protected override void WeaponControl()
    {
        if (!PauseMenu.GameIsPaused) {
            weaponAimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
                weaponChooseNext = true;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                weaponChoosePrev = true;
            if (Input.GetButtonDown("WeaponSlot1"))
                weaponChooseFirst = true;
            else if (Input.GetButtonDown("WeaponSlot2"))
                weaponChooseSecond = true;
            else if (Input.GetButtonDown("WeaponSlot3"))
                weaponChooseThird = true;
            else if (Input.GetButtonDown("WeaponSlot4"))
                weaponChooseFourth = true;
            if (Input.GetButtonDown("ChangeWeaponState"))
                weaponChangeState = true;
            if (Input.GetButtonDown("Reload"))
                weaponReload = true;
            if (Input.GetButton("Fire1"))
                weaponAttack = true;
            if (Input.GetButtonDown("Throw"))
                weaponThrowPress = true;
            if (Input.GetButtonUp("Throw"))
                weaponThrowRelease = true;
        }
    }
    protected override void WeaponFixedControl()
    {
        Inventory.Aim(weaponAimPoint);

        if (weaponChooseNext) {
            Inventory.ActiveSlot++;
            weaponChooseNext = false;
        } else if (weaponChoosePrev) {
            Inventory.ActiveSlot--;
            weaponChoosePrev = false;
        }
        if (weaponChooseFirst) {
            Inventory.ActiveSlot = Inventory.Slots.FIRST;
            weaponChooseFirst = false;
        } else if (weaponChooseSecond) {
            Inventory.ActiveSlot = Inventory.Slots.SECOND;
            weaponChooseSecond = false;
        } else if (weaponChooseThird) {
            Inventory.ActiveSlot = Inventory.Slots.THIRD;
            weaponChooseThird = false;
        } else if (weaponChooseFourth) {
            Inventory.ActiveSlot = Inventory.Slots.FOURTH;
            weaponChooseFourth = false;
        }
        if (weaponChangeState) {
            Inventory.ChangeWeaponState();
            weaponChangeState = false;
        }
        if (weaponReload) {
            Inventory.ReloadGun();
            weaponReload = false;
        }
        if (weaponAttack) {
            Inventory.AttackWithWeaponOrFist();
            weaponAttack = false;
        }
        if (weaponThrowPress) {
            Inventory.ThrowPress();
            weaponThrowPress = false;
        }
        if (weaponThrowRelease) {
            Inventory.ThrowRelease();
            weaponThrowRelease = false;
        }
    }

    #endregion

    /// <summary>
    /// Some code to indicate checkers
    /// </summary>
    protected void OnDrawGizmos() 
    {
        Gizmos.color = Color.grey;
        foreach (Transform tr in GroundCheckers) {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
        Gizmos.color = Color.green;
        foreach (Transform tr in RightSideCheckers) {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
        Gizmos.color = Color.yellow;
        foreach (Transform tr in LeftSideCheckers) {
            Gizmos.DrawSphere(tr.position, 0.1f);
        }
    }

}
