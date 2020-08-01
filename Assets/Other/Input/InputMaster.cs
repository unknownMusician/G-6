// GENERATED AUTOMATICALLY FROM 'Assets/Other/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""65665b65-271f-46a6-87be-d006d89171a9"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c021430f-d850-4d44-8afa-eaef4bc0bf03"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""13f3928e-fdb3-4ef3-9b37-3e59949b710b"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""35e9cc50-62dc-48e7-b13e-cc9927a6859f"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""a0717508-a040-4769-902e-3d74182df511"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeWeaponState"",
                    ""type"": ""Button"",
                    ""id"": ""8daeed15-af27-4706-a5f3-5f334f3ddc98"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""23bda3a8-9ca7-4a7d-b10d-665e081b5dc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowPress"",
                    ""type"": ""Button"",
                    ""id"": ""44a03c58-aca7-4b81-97fd-c6eca422e05e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowRelease"",
                    ""type"": ""Button"",
                    ""id"": ""fa3ed30a-6244-4e7b-bb47-05dc71339b0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slot1"",
                    ""type"": ""Button"",
                    ""id"": ""200f56a6-7408-4466-b48e-5579d74e6f7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slot2"",
                    ""type"": ""Button"",
                    ""id"": ""8dab73d7-1622-4056-8a64-71bbb3a076a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slot3"",
                    ""type"": ""Button"",
                    ""id"": ""57882988-c62b-4177-bd3a-75774986ab92"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Slot4"",
                    ""type"": ""Button"",
                    ""id"": ""bda44f08-c582-490f-86dd-f60a680c16dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeSlot"",
                    ""type"": ""Button"",
                    ""id"": ""3be61b4c-2fcd-4330-bbee-13216ec1e025"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5d1c748-cc61-4529-8382-9747146d72a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3fa59355-6976-4a4a-946b-2ad664c5de4a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ChangeWeaponState"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ede2c8e-48ff-4108-9c22-47dfcded38d7"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ThrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7acce02b-46f2-41c0-a1da-8ce0e19dcc39"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e867a0f4-380c-4cb9-8d10-a0ad1bbf767e"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slot3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df991077-f2b1-4a60-bf4c-81ad0e81c992"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slot4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MouseScroll"",
                    ""id"": ""97accb26-bb3f-412c-a979-95e2710c4d9d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeSlot"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f9f846f5-9310-4a9b-8169-cd2b9c69bf6c"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ChangeSlot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""beacf1a1-e65c-45b6-b9aa-d70754b3f17b"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ChangeSlot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""db83d568-2e13-4f9b-a4a9-26edc186f100"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ThrowRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71601a69-0a4c-44d3-b6ee-e768baa72988"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Slot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1384e046-a4e8-4d59-a730-ec301e19085a"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""7bdf531a-284e-468d-aa69-89e2bdec2119"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""a0a4d55f-27f4-48ef-8f06-d4df8f74c5e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a61ec04d-7897-44cd-9115-53b0f21650ce"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Attack = m_Weapon.FindAction("Attack", throwIfNotFound: true);
        m_Weapon_ChangeWeaponState = m_Weapon.FindAction("ChangeWeaponState", throwIfNotFound: true);
        m_Weapon_Reload = m_Weapon.FindAction("Reload", throwIfNotFound: true);
        m_Weapon_ThrowPress = m_Weapon.FindAction("ThrowPress", throwIfNotFound: true);
        m_Weapon_ThrowRelease = m_Weapon.FindAction("ThrowRelease", throwIfNotFound: true);
        m_Weapon_Slot1 = m_Weapon.FindAction("Slot1", throwIfNotFound: true);
        m_Weapon_Slot2 = m_Weapon.FindAction("Slot2", throwIfNotFound: true);
        m_Weapon_Slot3 = m_Weapon.FindAction("Slot3", throwIfNotFound: true);
        m_Weapon_Slot4 = m_Weapon.FindAction("Slot4", throwIfNotFound: true);
        m_Weapon_ChangeSlot = m_Weapon.FindAction("ChangeSlot", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Jump;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Attack;
    private readonly InputAction m_Weapon_ChangeWeaponState;
    private readonly InputAction m_Weapon_Reload;
    private readonly InputAction m_Weapon_ThrowPress;
    private readonly InputAction m_Weapon_ThrowRelease;
    private readonly InputAction m_Weapon_Slot1;
    private readonly InputAction m_Weapon_Slot2;
    private readonly InputAction m_Weapon_Slot3;
    private readonly InputAction m_Weapon_Slot4;
    private readonly InputAction m_Weapon_ChangeSlot;
    public struct WeaponActions
    {
        private @InputMaster m_Wrapper;
        public WeaponActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_Weapon_Attack;
        public InputAction @ChangeWeaponState => m_Wrapper.m_Weapon_ChangeWeaponState;
        public InputAction @Reload => m_Wrapper.m_Weapon_Reload;
        public InputAction @ThrowPress => m_Wrapper.m_Weapon_ThrowPress;
        public InputAction @ThrowRelease => m_Wrapper.m_Weapon_ThrowRelease;
        public InputAction @Slot1 => m_Wrapper.m_Weapon_Slot1;
        public InputAction @Slot2 => m_Wrapper.m_Weapon_Slot2;
        public InputAction @Slot3 => m_Wrapper.m_Weapon_Slot3;
        public InputAction @Slot4 => m_Wrapper.m_Weapon_Slot4;
        public InputAction @ChangeSlot => m_Wrapper.m_Weapon_ChangeSlot;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttack;
                @ChangeWeaponState.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeWeaponState;
                @ChangeWeaponState.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeWeaponState;
                @ChangeWeaponState.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeWeaponState;
                @Reload.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @ThrowPress.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowPress;
                @ThrowPress.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowPress;
                @ThrowPress.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowPress;
                @ThrowRelease.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowRelease;
                @ThrowRelease.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowRelease;
                @ThrowRelease.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowRelease;
                @Slot1.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot1;
                @Slot1.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot1;
                @Slot1.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot1;
                @Slot2.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot2;
                @Slot2.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot2;
                @Slot2.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot2;
                @Slot3.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot3;
                @Slot3.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot3;
                @Slot3.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot3;
                @Slot4.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot4;
                @Slot4.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot4;
                @Slot4.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSlot4;
                @ChangeSlot.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeSlot;
                @ChangeSlot.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeSlot;
                @ChangeSlot.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnChangeSlot;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @ChangeWeaponState.started += instance.OnChangeWeaponState;
                @ChangeWeaponState.performed += instance.OnChangeWeaponState;
                @ChangeWeaponState.canceled += instance.OnChangeWeaponState;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @ThrowPress.started += instance.OnThrowPress;
                @ThrowPress.performed += instance.OnThrowPress;
                @ThrowPress.canceled += instance.OnThrowPress;
                @ThrowRelease.started += instance.OnThrowRelease;
                @ThrowRelease.performed += instance.OnThrowRelease;
                @ThrowRelease.canceled += instance.OnThrowRelease;
                @Slot1.started += instance.OnSlot1;
                @Slot1.performed += instance.OnSlot1;
                @Slot1.canceled += instance.OnSlot1;
                @Slot2.started += instance.OnSlot2;
                @Slot2.performed += instance.OnSlot2;
                @Slot2.canceled += instance.OnSlot2;
                @Slot3.started += instance.OnSlot3;
                @Slot3.performed += instance.OnSlot3;
                @Slot3.canceled += instance.OnSlot3;
                @Slot4.started += instance.OnSlot4;
                @Slot4.performed += instance.OnSlot4;
                @Slot4.canceled += instance.OnSlot4;
                @ChangeSlot.started += instance.OnChangeSlot;
                @ChangeSlot.performed += instance.OnChangeSlot;
                @ChangeSlot.canceled += instance.OnChangeSlot;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Newaction;
    public struct UIActions
    {
        private @InputMaster m_Wrapper;
        public UIActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnChangeWeaponState(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnThrowPress(InputAction.CallbackContext context);
        void OnThrowRelease(InputAction.CallbackContext context);
        void OnSlot1(InputAction.CallbackContext context);
        void OnSlot2(InputAction.CallbackContext context);
        void OnSlot3(InputAction.CallbackContext context);
        void OnSlot4(InputAction.CallbackContext context);
        void OnChangeSlot(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
