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
                    ""id"": ""6ab9932e-4305-420c-acc2-40548db54192"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""360dc539-8912-4997-aabb-141ac7f7cea1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Button"",
                    ""id"": ""fc013bf8-33ac-4894-bacf-9a62d97fa9de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoSneak"",
                    ""type"": ""Button"",
                    ""id"": ""20487d3a-c7db-4c62-b435-ef8520424379"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""1e79f1c0-99dc-4595-bcc2-86b5e27c5aab"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoRun"",
                    ""type"": ""Value"",
                    ""id"": ""bc223abc-bbf9-4ed6-8871-9e7dbb5f2a5d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RunChange"",
                    ""type"": ""Value"",
                    ""id"": ""769feebe-6081-4b74-b4ff-4e6d32e42bbb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f1018985-9da2-431e-895b-6869be540d66"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoMove"",
                    ""type"": ""Value"",
                    ""id"": ""53a4ce03-f8a9-4594-8bf1-7d1fb051d901"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0f452493-5968-4f6a-b907-4038e69f83c0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c25b6dd9-2822-4a4e-a0cc-917030ceab26"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56ae5782-7d7e-4764-ac8e-963516c50db9"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""771a1ce0-9de6-4553-9ee9-0589f4cf570e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82afde1e-fa51-465d-b8f3-5eff1ba4e7fd"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e2b8ced-66e1-44bc-8587-2cc38c1bf042"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5071681b-5890-43a2-8fd3-34977a2d7df2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a25d85d-b3ec-421f-9e8f-068e6f727c99"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4cbf3bd-fd35-445f-b28f-4e42f4cf88ec"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoSneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4159e49d-1a83-4a56-8317-8d7235f87f8b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoRun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ad1ded4-50ec-4dbb-99dc-c6d6def5aec5"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75669ef8-760f-4df2-bd1b-e6dc105cf476"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoSneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""12b16545-8c07-4d23-8ece-2960aa0b29cc"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14725c25-100f-483c-bbdf-d93323a793b7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""059b01c8-a196-4544-96be-800ac3aba2c5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fb0ca166-a493-400a-b18c-2b0ecf42945b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""baf4563c-e5d3-4f51-9be5-837d19bf5307"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""8941faa9-b3b3-4016-8e49-d22f7f1aa0c7"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c124ba5d-93d9-42db-bff9-a195dfb86106"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ec8d3ea4-535e-4d54-9fad-eeb0149fe89a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ead6e430-6217-4d02-ac28-ee86569c58b4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8f970a5b-846f-45da-beae-f9767016b6dd"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8abebb29-252d-4c5a-b64b-b22639ddc868"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8e21bdae-0718-4418-a2db-daea3e11c8a4"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a25e3062-de7e-4933-9aaf-ff790498d8ee"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""149cf572-0a28-49e2-9d4a-19b9ff30473a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a2337705-517a-46d9-b523-2d6f573a3f94"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""77894c22-89ef-42e4-bfbd-fd0f5de7d3ac"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""1e454a24-72e4-4bb3-a6fe-2ea92b698644"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6cf9935b-00f9-4ba8-b662-8e324b71a0a9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6a280771-7738-4426-9664-7879334bb331"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ce10494f-36c6-4e7b-b9c0-1cc7cf5e80b2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""26fb0926-fee5-4814-a0fb-d4abcd50ed1b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""73e0b2e8-e7a6-43bc-bcdd-e3a5fcd2fcac"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""NoMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49d607cd-14c4-495f-9c07-1fe5428d7f30"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""RunChange"",
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
                    ""name"": ""AttackPress"",
                    ""type"": ""Value"",
                    ""id"": ""a0717508-a040-4769-902e-3d74182df511"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackRelease"",
                    ""type"": ""Value"",
                    ""id"": ""fe506bda-b65c-40ab-9d2f-80b0ddf4c923"",
                    ""expectedControlType"": """",
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
                    ""type"": ""Value"",
                    ""id"": ""3be61b4c-2fcd-4330-bbee-13216ec1e025"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimMouse"",
                    ""type"": ""Value"",
                    ""id"": ""be8476f9-270b-4614-ac8a-c5056aa813f5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AimStick"",
                    ""type"": ""Value"",
                    ""id"": ""8e80a134-5860-4b53-b0f0-703c72dec5e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5d1c748-cc61-4529-8382-9747146d72a0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackPress"",
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
                    ""id"": ""f773cc07-1118-48b5-a8c2-1175fe45d95c"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
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
                    ""name"": """",
                    ""id"": ""9d189e9e-2103-4d55-baa8-642ff7941321"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ChangeSlot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
                    ""id"": ""059ddc9b-c708-4815-a8c6-9d73cbd64273"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""d93c132b-0d3a-4dbe-9d0a-7a5c8b3aa035"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AttackRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dabede61-68b4-440e-9141-e204e007e6c9"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f2abdad-9ac2-45f8-bff1-9549ae287693"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d2b06b2-dbeb-44ff-8b41-b1df7841ae72"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""AttackPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b1b9b60-787e-4127-b761-cadcf993c5bf"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""AttackRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""253968e2-260d-4384-805a-abee4d7bb8a3"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""ThrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edeed226-b13a-499f-b57f-446825f10263"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""ThrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ff90dcb-261d-407d-a14f-f35c7f4d278d"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""ThrowRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c106b5a4-bf47-4827-bfbf-172fee942eaa"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""ThrowRelease"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1cbe30b1-4746-4016-a43d-fa62e1552927"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Slot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a224af9a-6f99-471b-8c4f-e62a22f5f89a"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Slot3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17ada34f-bb56-4101-8eca-2d1a6f773ade"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Slot4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f638657-8e1d-4b6e-85ed-1c719c75f4a0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""AimStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9013bdf-a170-4079-90a9-d91a67856a67"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""AimMouse"",
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
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""8db5dbb2-9729-444a-9da2-a5fa67532883"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WeaponSettings"",
                    ""type"": ""Button"",
                    ""id"": ""7b095397-5c03-4d76-ac40-130fd1809491"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c0e9f17a-c3db-40d8-a1fa-e3c7dac3c0b9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""964c4d31-f132-441a-bb11-6d38e72a7cbe"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac764ceb-ef03-43d8-a69e-8567b1eb0765"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""WeaponSettings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2ca7cc1-d9c6-46eb-b6ce-8a220a8f8cea"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""PS4;Xbox Gamepad"",
                    ""action"": ""WeaponSettings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""EnvironmentBuilder"",
            ""id"": ""61504643-8eb5-43ed-b457-9ebb7bf5172f"",
            ""actions"": [
                {
                    ""name"": ""PlaceObject"",
                    ""type"": ""Button"",
                    ""id"": ""84ce3089-e322-4e89-9c26-5e367060f7d4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoPlaceObject"",
                    ""type"": ""Button"",
                    ""id"": ""ea0d4c30-ca2d-4cad-923e-eda2edf61151"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DeleteObject"",
                    ""type"": ""Button"",
                    ""id"": ""685276f5-2a16-44c6-b650-81815338c5ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NoDeleteObject"",
                    ""type"": ""Button"",
                    ""id"": ""4c477f48-429f-47f2-91eb-5ae9aa9ef428"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d57a5d11-2185-49af-9406-7dddeb1d64b8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c11ab2ff-002b-4162-809f-be86663ac297"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DeleteObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ebf94c9-db32-439c-905d-b067a9436c9e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoPlaceObject"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63e56a91-d1eb-412f-9f0f-6abb122838c7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NoDeleteObject"",
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
        },
        {
            ""name"": ""Xbox Gamepad"",
            ""bindingGroup"": ""Xbox Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PS4"",
            ""bindingGroup"": ""PS4"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShockGamepad>"",
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
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_Sneak = m_Player.FindAction("Sneak", throwIfNotFound: true);
        m_Player_NoSneak = m_Player.FindAction("NoSneak", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_NoRun = m_Player.FindAction("NoRun", throwIfNotFound: true);
        m_Player_RunChange = m_Player.FindAction("RunChange", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_NoMove = m_Player.FindAction("NoMove", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_AttackPress = m_Weapon.FindAction("AttackPress", throwIfNotFound: true);
        m_Weapon_AttackRelease = m_Weapon.FindAction("AttackRelease", throwIfNotFound: true);
        m_Weapon_ChangeWeaponState = m_Weapon.FindAction("ChangeWeaponState", throwIfNotFound: true);
        m_Weapon_Reload = m_Weapon.FindAction("Reload", throwIfNotFound: true);
        m_Weapon_ThrowPress = m_Weapon.FindAction("ThrowPress", throwIfNotFound: true);
        m_Weapon_ThrowRelease = m_Weapon.FindAction("ThrowRelease", throwIfNotFound: true);
        m_Weapon_Slot1 = m_Weapon.FindAction("Slot1", throwIfNotFound: true);
        m_Weapon_Slot2 = m_Weapon.FindAction("Slot2", throwIfNotFound: true);
        m_Weapon_Slot3 = m_Weapon.FindAction("Slot3", throwIfNotFound: true);
        m_Weapon_Slot4 = m_Weapon.FindAction("Slot4", throwIfNotFound: true);
        m_Weapon_ChangeSlot = m_Weapon.FindAction("ChangeSlot", throwIfNotFound: true);
        m_Weapon_AimMouse = m_Weapon.FindAction("AimMouse", throwIfNotFound: true);
        m_Weapon_AimStick = m_Weapon.FindAction("AimStick", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Menu = m_UI.FindAction("Menu", throwIfNotFound: true);
        m_UI_WeaponSettings = m_UI.FindAction("WeaponSettings", throwIfNotFound: true);
        // EnvironmentBuilder
        m_EnvironmentBuilder = asset.FindActionMap("EnvironmentBuilder", throwIfNotFound: true);
        m_EnvironmentBuilder_PlaceObject = m_EnvironmentBuilder.FindAction("PlaceObject", throwIfNotFound: true);
        m_EnvironmentBuilder_NoPlaceObject = m_EnvironmentBuilder.FindAction("NoPlaceObject", throwIfNotFound: true);
        m_EnvironmentBuilder_DeleteObject = m_EnvironmentBuilder.FindAction("DeleteObject", throwIfNotFound: true);
        m_EnvironmentBuilder_NoDeleteObject = m_EnvironmentBuilder.FindAction("NoDeleteObject", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_Sneak;
    private readonly InputAction m_Player_NoSneak;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_NoRun;
    private readonly InputAction m_Player_RunChange;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_NoMove;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Sneak => m_Wrapper.m_Player_Sneak;
        public InputAction @NoSneak => m_Wrapper.m_Player_NoSneak;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @NoRun => m_Wrapper.m_Player_NoRun;
        public InputAction @RunChange => m_Wrapper.m_Player_RunChange;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @NoMove => m_Wrapper.m_Player_NoMove;
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
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Sneak.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSneak;
                @Sneak.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSneak;
                @Sneak.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSneak;
                @NoSneak.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoSneak;
                @NoSneak.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoSneak;
                @NoSneak.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoSneak;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @NoRun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoRun;
                @NoRun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoRun;
                @NoRun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoRun;
                @RunChange.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunChange;
                @RunChange.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunChange;
                @RunChange.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRunChange;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @NoMove.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoMove;
                @NoMove.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoMove;
                @NoMove.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNoMove;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Sneak.started += instance.OnSneak;
                @Sneak.performed += instance.OnSneak;
                @Sneak.canceled += instance.OnSneak;
                @NoSneak.started += instance.OnNoSneak;
                @NoSneak.performed += instance.OnNoSneak;
                @NoSneak.canceled += instance.OnNoSneak;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @NoRun.started += instance.OnNoRun;
                @NoRun.performed += instance.OnNoRun;
                @NoRun.canceled += instance.OnNoRun;
                @RunChange.started += instance.OnRunChange;
                @RunChange.performed += instance.OnRunChange;
                @RunChange.canceled += instance.OnRunChange;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @NoMove.started += instance.OnNoMove;
                @NoMove.performed += instance.OnNoMove;
                @NoMove.canceled += instance.OnNoMove;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_AttackPress;
    private readonly InputAction m_Weapon_AttackRelease;
    private readonly InputAction m_Weapon_ChangeWeaponState;
    private readonly InputAction m_Weapon_Reload;
    private readonly InputAction m_Weapon_ThrowPress;
    private readonly InputAction m_Weapon_ThrowRelease;
    private readonly InputAction m_Weapon_Slot1;
    private readonly InputAction m_Weapon_Slot2;
    private readonly InputAction m_Weapon_Slot3;
    private readonly InputAction m_Weapon_Slot4;
    private readonly InputAction m_Weapon_ChangeSlot;
    private readonly InputAction m_Weapon_AimMouse;
    private readonly InputAction m_Weapon_AimStick;
    public struct WeaponActions
    {
        private @InputMaster m_Wrapper;
        public WeaponActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @AttackPress => m_Wrapper.m_Weapon_AttackPress;
        public InputAction @AttackRelease => m_Wrapper.m_Weapon_AttackRelease;
        public InputAction @ChangeWeaponState => m_Wrapper.m_Weapon_ChangeWeaponState;
        public InputAction @Reload => m_Wrapper.m_Weapon_Reload;
        public InputAction @ThrowPress => m_Wrapper.m_Weapon_ThrowPress;
        public InputAction @ThrowRelease => m_Wrapper.m_Weapon_ThrowRelease;
        public InputAction @Slot1 => m_Wrapper.m_Weapon_Slot1;
        public InputAction @Slot2 => m_Wrapper.m_Weapon_Slot2;
        public InputAction @Slot3 => m_Wrapper.m_Weapon_Slot3;
        public InputAction @Slot4 => m_Wrapper.m_Weapon_Slot4;
        public InputAction @ChangeSlot => m_Wrapper.m_Weapon_ChangeSlot;
        public InputAction @AimMouse => m_Wrapper.m_Weapon_AimMouse;
        public InputAction @AimStick => m_Wrapper.m_Weapon_AimStick;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @AttackPress.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackPress;
                @AttackPress.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackPress;
                @AttackPress.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackPress;
                @AttackRelease.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackRelease;
                @AttackRelease.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackRelease;
                @AttackRelease.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAttackRelease;
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
                @AimMouse.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimMouse;
                @AimMouse.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimMouse;
                @AimMouse.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimMouse;
                @AimStick.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimStick;
                @AimStick.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimStick;
                @AimStick.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAimStick;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AttackPress.started += instance.OnAttackPress;
                @AttackPress.performed += instance.OnAttackPress;
                @AttackPress.canceled += instance.OnAttackPress;
                @AttackRelease.started += instance.OnAttackRelease;
                @AttackRelease.performed += instance.OnAttackRelease;
                @AttackRelease.canceled += instance.OnAttackRelease;
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
                @AimMouse.started += instance.OnAimMouse;
                @AimMouse.performed += instance.OnAimMouse;
                @AimMouse.canceled += instance.OnAimMouse;
                @AimStick.started += instance.OnAimStick;
                @AimStick.performed += instance.OnAimStick;
                @AimStick.canceled += instance.OnAimStick;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Menu;
    private readonly InputAction m_UI_WeaponSettings;
    public struct UIActions
    {
        private @InputMaster m_Wrapper;
        public UIActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_UI_Menu;
        public InputAction @WeaponSettings => m_Wrapper.m_UI_WeaponSettings;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Menu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMenu;
                @WeaponSettings.started -= m_Wrapper.m_UIActionsCallbackInterface.OnWeaponSettings;
                @WeaponSettings.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnWeaponSettings;
                @WeaponSettings.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnWeaponSettings;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @WeaponSettings.started += instance.OnWeaponSettings;
                @WeaponSettings.performed += instance.OnWeaponSettings;
                @WeaponSettings.canceled += instance.OnWeaponSettings;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // EnvironmentBuilder
    private readonly InputActionMap m_EnvironmentBuilder;
    private IEnvironmentBuilderActions m_EnvironmentBuilderActionsCallbackInterface;
    private readonly InputAction m_EnvironmentBuilder_PlaceObject;
    private readonly InputAction m_EnvironmentBuilder_NoPlaceObject;
    private readonly InputAction m_EnvironmentBuilder_DeleteObject;
    private readonly InputAction m_EnvironmentBuilder_NoDeleteObject;
    public struct EnvironmentBuilderActions
    {
        private @InputMaster m_Wrapper;
        public EnvironmentBuilderActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlaceObject => m_Wrapper.m_EnvironmentBuilder_PlaceObject;
        public InputAction @NoPlaceObject => m_Wrapper.m_EnvironmentBuilder_NoPlaceObject;
        public InputAction @DeleteObject => m_Wrapper.m_EnvironmentBuilder_DeleteObject;
        public InputAction @NoDeleteObject => m_Wrapper.m_EnvironmentBuilder_NoDeleteObject;
        public InputActionMap Get() { return m_Wrapper.m_EnvironmentBuilder; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EnvironmentBuilderActions set) { return set.Get(); }
        public void SetCallbacks(IEnvironmentBuilderActions instance)
        {
            if (m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface != null)
            {
                @PlaceObject.started -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnPlaceObject;
                @PlaceObject.performed -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnPlaceObject;
                @PlaceObject.canceled -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnPlaceObject;
                @NoPlaceObject.started -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoPlaceObject;
                @NoPlaceObject.performed -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoPlaceObject;
                @NoPlaceObject.canceled -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoPlaceObject;
                @DeleteObject.started -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnDeleteObject;
                @DeleteObject.performed -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnDeleteObject;
                @DeleteObject.canceled -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnDeleteObject;
                @NoDeleteObject.started -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoDeleteObject;
                @NoDeleteObject.performed -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoDeleteObject;
                @NoDeleteObject.canceled -= m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface.OnNoDeleteObject;
            }
            m_Wrapper.m_EnvironmentBuilderActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlaceObject.started += instance.OnPlaceObject;
                @PlaceObject.performed += instance.OnPlaceObject;
                @PlaceObject.canceled += instance.OnPlaceObject;
                @NoPlaceObject.started += instance.OnNoPlaceObject;
                @NoPlaceObject.performed += instance.OnNoPlaceObject;
                @NoPlaceObject.canceled += instance.OnNoPlaceObject;
                @DeleteObject.started += instance.OnDeleteObject;
                @DeleteObject.performed += instance.OnDeleteObject;
                @DeleteObject.canceled += instance.OnDeleteObject;
                @NoDeleteObject.started += instance.OnNoDeleteObject;
                @NoDeleteObject.performed += instance.OnNoDeleteObject;
                @NoDeleteObject.canceled += instance.OnNoDeleteObject;
            }
        }
    }
    public EnvironmentBuilderActions @EnvironmentBuilder => new EnvironmentBuilderActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_XboxGamepadSchemeIndex = -1;
    public InputControlScheme XboxGamepadScheme
    {
        get
        {
            if (m_XboxGamepadSchemeIndex == -1) m_XboxGamepadSchemeIndex = asset.FindControlSchemeIndex("Xbox Gamepad");
            return asset.controlSchemes[m_XboxGamepadSchemeIndex];
        }
    }
    private int m_PS4SchemeIndex = -1;
    public InputControlScheme PS4Scheme
    {
        get
        {
            if (m_PS4SchemeIndex == -1) m_PS4SchemeIndex = asset.FindControlSchemeIndex("PS4");
            return asset.controlSchemes[m_PS4SchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnSneak(InputAction.CallbackContext context);
        void OnNoSneak(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnNoRun(InputAction.CallbackContext context);
        void OnRunChange(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnNoMove(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnAttackPress(InputAction.CallbackContext context);
        void OnAttackRelease(InputAction.CallbackContext context);
        void OnChangeWeaponState(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnThrowPress(InputAction.CallbackContext context);
        void OnThrowRelease(InputAction.CallbackContext context);
        void OnSlot1(InputAction.CallbackContext context);
        void OnSlot2(InputAction.CallbackContext context);
        void OnSlot3(InputAction.CallbackContext context);
        void OnSlot4(InputAction.CallbackContext context);
        void OnChangeSlot(InputAction.CallbackContext context);
        void OnAimMouse(InputAction.CallbackContext context);
        void OnAimStick(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnWeaponSettings(InputAction.CallbackContext context);
    }
    public interface IEnvironmentBuilderActions
    {
        void OnPlaceObject(InputAction.CallbackContext context);
        void OnNoPlaceObject(InputAction.CallbackContext context);
        void OnDeleteObject(InputAction.CallbackContext context);
        void OnNoDeleteObject(InputAction.CallbackContext context);
    }
}
