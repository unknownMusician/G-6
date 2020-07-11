using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : CharacterBase
{
    new private void Start()
    {

        MaxHP = 100f;
        MaxSP = 100f;
        MaxOP = 100f;

        HP = MaxHP;
        SP = MaxSP;
        OP = MaxOP;

        base.Start();
    }

    new protected void Update()
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

    protected override void WeaponControl()
    {
        // To-Do
    }
    protected override void WeaponFixedControl()
    {
        Inventory.Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            Inventory.ChooseNext();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            Inventory.ChoosePrev();
        }
        if (Input.GetButtonDown("WeaponSlot1")) {
            Inventory.Choose(Inventory.Slots.FIRST);
        } else if (Input.GetButtonDown("WeaponSlot2")) {
            Inventory.Choose(Inventory.Slots.SECOND);
        } else if (Input.GetButtonDown("WeaponSlot3")) {
            Inventory.Choose(Inventory.Slots.THIRD);
        } else if (Input.GetButtonDown("WeaponSlot4")) {
            Inventory.Choose(Inventory.Slots.FOURTH);
        }
        if (Inventory.Weapon != null) {
            if (Input.GetButtonDown("ChangeWeaponState")) {
                Inventory.Weapon.ChangeState();
            }
            if (Input.GetButtonDown("Reload")) {
                Inventory.Reload();
            }
            if (Input.GetButton("Fire1"))
                Inventory.Weapon.Attack();
        }
        if (Input.GetButtonDown("Throw"))
            Inventory.ThrowPress();
        if (Input.GetButtonUp("Throw"))
            Inventory.ThrowRelease();
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
