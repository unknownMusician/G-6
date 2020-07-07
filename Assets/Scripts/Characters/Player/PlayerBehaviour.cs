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
        Hand.Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Inventory.ChooseNext();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Inventory.ChoosePrev();
        }
        if (Input.GetButtonDown("WeaponSlot1"))
        {
            Inventory.Choose(Inventory.Slots.first);
        }
        else if (Input.GetButtonDown("WeaponSlot2"))
        {
            Inventory.Choose(Inventory.Slots.second);
        }
        else if (Input.GetButtonDown("WeaponSlot3"))
        {
            Inventory.Choose(Inventory.Slots.third);
        }
        else if (Input.GetButtonDown("WeaponSlot4"))
        {
            Inventory.Choose(Inventory.Slots.fourth);
        }
        if (Inventory.Weapon != null)
        {
            if (Input.GetButtonDown("ChangeWeaponState"))
            {
                Inventory.Weapon.ChangeState();
            }
            if (Input.GetButtonDown("Reload"))
            {
                Inventory.Weapon.Reload();
            }
        }
    }
    protected override void WeaponFixedControl()
    {
        if (Inventory.Weapon != null)
        {
            if (Input.GetButton("Fire1"))
                Inventory.Weapon.Attack();
        }
        if (Input.GetButtonDown("Throw"))
            Inventory.ThrowPress();
        if (Input.GetButtonUp("Throw"))
            Inventory.ThrowRelease();
    }

    #endregion
}
