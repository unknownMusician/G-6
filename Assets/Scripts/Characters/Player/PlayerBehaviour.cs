using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : CharacterBase
{

    public InputMaster controls;

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

        // Controls
        controls = new InputMaster();

        // Weapon Controls
        controls.Weapon.AttackPress.performed += ctx => InputAttackPress();
        controls.Weapon.AttackRelease.performed += ctx => InputAttackRelease();
        controls.Weapon.ChangeWeaponState.performed += ctx => InputChangeState();
        controls.Weapon.Reload.performed += ctx => InputReload();
        controls.Weapon.ThrowPress.performed += ctx => InputThrowPress();
        controls.Weapon.ThrowRelease.performed += ctx => InputThrowRelease();
        controls.Weapon.Slot1.performed += ctx => InputWeaponSlot(0);
        controls.Weapon.Slot2.performed += ctx => InputWeaponSlot(1);
        controls.Weapon.Slot3.performed += ctx => InputWeaponSlot(2);
        controls.Weapon.Slot4.performed += ctx => InputWeaponSlot(3);
        controls.Weapon.ChangeSlot.performed += ctx => InputWeaponSlotPrevNext();
        controls.Weapon.Aim.performed += ctx => InputAim();

    }
    private void OnEnable()
    {
        controls.Weapon.Enable();
    }
    private void OnDisable()
    {
        controls.Weapon.Disable();
    }
    private new void Start()
    {

        MaxHP = 100f;
        MaxSP = 100f;
        MaxOP = 100f;

        base.Start();

        CurrentEffects[CardEffect.EffectType.Fire] = new EffectControl(
            new CardEffect.NestedProps(
                CardEffect.EffectType.Fire,
                1,
                20,
                1
                ), this);
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

            //if ((Input.GetButton("Horizontal") || Input.GetButtonDown("Jump"))) // To-Do: change to buttons and actions
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

    #region WeaponControl

    private bool weaponChooseNext = false;
    private bool weaponChoosePrev = false;
    private bool weaponChooseFirst = false;
    private bool weaponChooseSecond = false;
    private bool weaponChooseThird = false;
    private bool weaponChooseFourth = false;
    private bool weaponChangeState = false;
    private bool weaponReload = false;
    private bool weaponAttackPress = false;
    private bool weaponAttackRelease = false;
    private bool weaponThrowPress = false;
    private bool weaponThrowRelease = false;
    private Vector3 weaponAimLocalPoint = Vector3.right;

    protected override void WeaponControl()
    {

    }
    protected override void WeaponFixedControl()
    {
        Inventory.Aim(Camera.main.ScreenToWorldPoint(weaponAimLocalPoint));

        if (weaponChooseNext)
        {
            Inventory.ActiveSlot++;
            weaponChooseNext = false;
        }
        else if (weaponChoosePrev)
        {
            Inventory.ActiveSlot--;
            weaponChoosePrev = false;
        }
        if (weaponChooseFirst)
        {
            Inventory.ActiveSlot = Inventory.Slots.FIRST;
            weaponChooseFirst = false;
        }
        else if (weaponChooseSecond)
        {
            Inventory.ActiveSlot = Inventory.Slots.SECOND;
            weaponChooseSecond = false;
        }
        else if (weaponChooseThird)
        {
            Inventory.ActiveSlot = Inventory.Slots.THIRD;
            weaponChooseThird = false;
        }
        else if (weaponChooseFourth)
        {
            Inventory.ActiveSlot = Inventory.Slots.FOURTH;
            weaponChooseFourth = false;
        }
        if (weaponChangeState)
        {
            Inventory.ChangeWeaponState();
            weaponChangeState = false;
        }
        if (weaponReload)
        {
            Inventory.ReloadGun();
            weaponReload = false;
        }
        if (weaponAttackPress)
        {
            Inventory.AttackWithWeaponOrFistPress();
            weaponAttackPress = false;
        }
        if (weaponAttackRelease)
        {
            Inventory.AttackWithWeaponOrFistRelease();
            weaponAttackRelease = false;
        }
        if (weaponThrowPress)
        {
            Inventory.ThrowPress();
            weaponThrowPress = false;
        }
        if (weaponThrowRelease)
        {
            Inventory.ThrowRelease();
            weaponThrowRelease = false;
        }
    }

    #endregion

    #region Input

    public void InputAttackPress() {
        if (!Pause.GameIsPaused)
            weaponAttackPress = true;
        Debug.Log("InputAttackPress");
    }

    public void InputAttackRelease() {
        if (!Pause.GameIsPaused)
            weaponAttackRelease = true;
        Debug.Log("InputAttackRelease");
    }

    public void InputChangeState() {
        if (!Pause.GameIsPaused)
            weaponChangeState = true;
    }

    public void InputReload() {
        if (!Pause.GameIsPaused)
            weaponReload = true;
    }

    public void InputThrowPress() {
        if (!Pause.GameIsPaused)
            weaponThrowPress = true;
    }

    public void InputThrowRelease() {
        if (!Pause.GameIsPaused)
            weaponThrowRelease = true;
    }

    public void InputWeaponSlot(int slot) {
        if (!Pause.GameIsPaused)
            switch (slot) {
                case 0:
                    weaponChooseFirst = true;
                    break;
                case 1:
                    weaponChooseSecond = true;
                    break;
                case 2:
                    weaponChooseThird = true;
                    break;
                case 3:
                    weaponChooseFourth = true;
                    break;
            }
    }

    public void InputWeaponSlotPrevNext() {
        if (!Pause.GameIsPaused) {
            if (Mouse.current.scroll.ReadValue().y > 0)
                weaponChooseNext = true;
            else if (Mouse.current.scroll.ReadValue().y < 0)
                weaponChoosePrev = true;
        }
    }

    public void InputAim() {
        if (!Pause.GameIsPaused)
            weaponAimLocalPoint = Mouse.current.position.ReadValue();
    }

    #endregion

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
