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
        MainData.PlayerObject = this.gameObject;

        #region Controls

        #region Weapon Controls
        MainData.Controls.Weapon.AttackPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackWithWeaponOrFistPress(); };
        MainData.Controls.Weapon.AttackRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory.AttackWithWeaponOrFistRelease(); };
        MainData.Controls.Weapon.ChangeWeaponState.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ChangeWeaponState(); };
        MainData.Controls.Weapon.Reload.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ReloadGun(); };
        MainData.Controls.Weapon.ThrowPress.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ThrowPress(); };
        MainData.Controls.Weapon.ThrowRelease.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ThrowRelease(); };
        MainData.Controls.Weapon.Slot1.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.FIRST; };
        MainData.Controls.Weapon.Slot2.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.SECOND; };
        MainData.Controls.Weapon.Slot3.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.THIRD; };
        MainData.Controls.Weapon.Slot4.performed += ctx => { if (!Pause.GameIsPaused) Inventory.ActiveSlot = Inventory.Slots.FOURTH; };
        MainData.Controls.Weapon.ChangeSlot.performed += ctx =>
        {
            if (!Pause.GameIsPaused)
                _ = Mouse.current.scroll.ReadValue().y < 0 ? Inventory.ActiveSlot-- : Inventory.ActiveSlot++;
        };
        MainData.Controls.Weapon.Aim.performed += ctx =>
        {
            if (!Pause.GameIsPaused)
                Inventory.Aim(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
        };
        #endregion

        #region Player

        MainData.Controls.Player.Jump.performed += ctx => { if (!Pause.GameIsPaused) Jump(); };

        MainData.Controls.Player.Sneak.performed += ctx => { if (!Pause.GameIsPaused) IsSneaking = true; };
        MainData.Controls.Player.Stand.performed += ctx => { if (!Pause.GameIsPaused) IsSneaking = false; };

        MainData.Controls.Player.Run.performed += ctx => { if (!Pause.GameIsPaused) IsRunning = true; };
        MainData.Controls.Player.Go.performed += ctx => { if (!Pause.GameIsPaused) IsRunning = false; };

        MainData.Controls.Player.Interact.performed += ctx => { if (!Pause.GameIsPaused) TryInteract(); };

        MainData.Controls.Player.MoveRight.performed += ctx => { if (!Pause.GameIsPaused) MoveX(1); };
        MainData.Controls.Player.MoveLeft.performed += ctx => { if (!Pause.GameIsPaused) MoveX(-1); };

        MainData.Controls.Player.MoveUp.performed += ctx => { if (!Pause.GameIsPaused) MoveY(1); };
        MainData.Controls.Player.MoveDown.performed += ctx => { if (!Pause.GameIsPaused) MoveY(-1); };

        MainData.Controls.Player.Stay.performed += ctx => { MoveY(0); MoveX(0); };

        MainData.Controls.Player.Jump.performed += ctx => { Console.WriteLine("Jump"); };


        #endregion

        #endregion
    }
    private void OnEnable()
    {
        MainData.Controls.Weapon.Enable();
        MainData.Controls.Player.Enable();
    }
    private void OnDisable()
    {
        MainData.Controls.Weapon.Disable();
        MainData.Controls.Player.Disable();
    }
    private new void Start()
    {

        MaxHP = 100f;
        MaxSP = 100f;
        MaxOP = 100f;

        base.Start();

        //CurrentEffects[CardEffect.EffectType.Fire] = new EffectControl(
        //    new CardEffect.NestedProps(
        //        CardEffect.EffectType.Fire,
        //        1,
        //        20,
        //        1
        //        ), this);
    }

    protected new void Update()
    {
        if (State != State.Dead)
        {
            base.Update();

            Side = CheckSideLR(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

            if (Keyboard.current.eKey.wasPressedThisFrame /*Input.GetButtonDown("Interact")*/) // To-Do: change to buttons and actions
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

            //if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump"))) // ToDo: change to buttons and actions
            //    MoveX(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"));

        }
        else
        {
            if (State != State.Climb)
            {
                //if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump")))
                //MoveX(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxisRaw("Run") > 0);  // To-Do: change to buttons and actions
            }
            else
            {
                //if ((Input.GetButton("Vertical") || Input.GetButtonDown("Jump")))
                //MoveY(Input.GetAxisRaw("Vertical"), Input.GetButtonDown("Jump")); // To-Do: change to buttons and actions
            }
        }
    }

    /// <summary>
    /// Some code to indicate checkers
    /// </summary>
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
