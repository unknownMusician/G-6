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
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Stand"",
                    ""type"": ""Button"",
                    ""id"": ""20487d3a-c7db-4c62-b435-ef8520424379"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""1e79f1c0-99dc-4595-bcc2-86b5e27c5aab"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Go"",
                    ""type"": ""Value"",
                    ""id"": ""bc223abc-bbf9-4ed6-8871-9e7dbb5f2a5d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""bc79ae5a-0713-4bc4-8d52-33f1f3ae3bfb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""701e4ce5-09ba-4473-9ca4-73a9eb1841d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""9da1391c-0468-49c7-bac4-f870acf7e050"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""187067dd-b457-4ac5-a3ef-7bd0004f5baf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Stay"",
                    ""type"": ""Button"",
                    ""id"": ""53a4ce03-f8a9-4594-8bf1-7d1fb051d901"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""MoveVertical"",
                    ""type"": ""Value"",
                    ""id"": ""1504ed1d-d570-495f-adaa-0dd2ec30d318"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
                },
                {
                    ""name"": ""MoveHorizontal"",
                    ""type"": ""Value"",
                    ""id"": ""d36e0ee2-d1b0-4ec2-8676-4f948ffe6f2f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=1)""
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
                    ""id"": ""9ad1ded4-50ec-4dbb-99dc-c6d6def5aec5"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82afde1e-fa51-465d-b8f3-5eff1ba4e7fd"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""771a1ce0-9de6-4553-9ee9-0589f4cf570e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44257061-2b35-4658-91db-53f1b72af6d7"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c321f2cc-8d0a-4b57-a41c-aee2b9882c0d"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e2b8ced-66e1-44bc-8587-2cc38c1bf042"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a25d85d-b3ec-421f-9e8f-068e6f727c99"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92b44a18-3cf3-448b-a817-512843aa63e6"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a5653cb-5b5c-40f0-a40e-4df5c4c23641"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad"",
                    ""action"": ""Stand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69a09cb6-ded6-4d27-98ee-44cbb476c0aa"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4"",
                    ""action"": ""Stand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4cbf3bd-fd35-445f-b28f-4e42f4cf88ec"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stand"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4159e49d-1a83-4a56-8317-8d7235f87f8b"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3af1247e-7ced-4271-b920-e958f06962d9"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebaa2c9d-0cc9-4028-b925-165b247860c8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f1d00b3-ca00-4e09-beac-093a0f1cb840"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ce95ba7-df18-4639-9709-a92950f14462"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c73cb1e-a40e-418a-ba8c-e4d6b2fe6f01"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24bc065c-3d55-4634-b9cb-b90348e876bd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0d0a71f-b40f-4e22-b3b9-c544a31fc036"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96ec23be-3897-499b-bc5a-d05c7546eae2"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc485616-6b58-428c-9515-80ab8755b383"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3301619-fc81-46ae-99b4-e50c05ffdfc7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e41bedf3-d8b3-48e3-b889-88cd9490b590"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8f3b7f2-be1d-4074-aaa3-3213850369d2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84d5a8de-1918-4de2-94db-9c60f969a933"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""794c6c1a-ba09-4bef-9333-08592b600ac6"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7512788-dd3d-4083-b839-d093be560791"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3d1a283-cfaf-4fd2-a932-a6096440791b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Gamepad;PS4"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9685e9c3-f8a8-4408-aead-43aa3f1805f6"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4;Xbox Gamepad"",
                    ""action"": ""Stay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d502ba1d-ab01-4201-af5b-005fcf960eb7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3bcfc895-a79a-4f8e-a67f-9fc0823b3a18"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5af4cbcc-04a3-4650-a65a-75d2cc830fab"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""59620105-8ed1-4a61-87fc-935460fe582f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9ebced5f-61c3-4173-b7ee-69e63ebf2659"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6d90b117-1ec6-43ee-9dd0-a4c4faccde97"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""65750d9d-cf3c-454d-8bb9-bab7a66ab7c8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""2ccf173b-3e3f-4f02-9ac1-361286b3f880"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8a66c08f-bd7b-4db3-b2b8-a1b0eaaaf9f1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""b81f0815-57b2-44be-88ae-533bf9a11285"",
                    ""path"": ""1DAxis"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5b8fbf84-5455-4a0d-a675-14b7ccf719aa"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a4563b9a-eb6e-4d6a-ad45-cd4cc49f984e"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""be8476f9-270b-4614-ac8a-c5056aa813f5"",
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
                    ""id"": ""d732de6d-aa76-47b7-a5bd-50c468e924b4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Aim"",
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
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac764ceb-ef03-43d8-a69e-8567b1eb0765"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""WeaponSettings"",
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
        m_Player_Stand = m_Player.FindAction("Stand", throwIfNotFound: true);
        m_Player_Run = m_Player.FindAction("Run", throwIfNotFound: true);
        m_Player_Go = m_Player.FindAction("Go", throwIfNotFound: true);
        m_Player_MoveRight = m_Player.FindAction("MoveRight", throwIfNotFound: true);
        m_Player_MoveLeft = m_Player.FindAction("MoveLeft", throwIfNotFound: true);
        m_Player_MoveUp = m_Player.FindAction("MoveUp", throwIfNotFound: true);
        m_Player_MoveDown = m_Player.FindAction("MoveDown", throwIfNotFound: true);
        m_Player_Stay = m_Player.FindAction("Stay", throwIfNotFound: true);
        m_Player_MoveVertical = m_Player.FindAction("MoveVertical", throwIfNotFound: true);
        m_Player_MoveHorizontal = m_Player.FindAction("MoveHorizontal", throwIfNotFound: true);
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
        m_Weapon_Aim = m_Weapon.FindAction("Aim", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Menu = m_UI.FindAction("Menu", throwIfNotFound: true);
        m_UI_WeaponSettings = m_UI.FindAction("WeaponSettings", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Stand;
    private readonly InputAction m_Player_Run;
    private readonly InputAction m_Player_Go;
    private readonly InputAction m_Player_MoveRight;
    private readonly InputAction m_Player_MoveLeft;
    private readonly InputAction m_Player_MoveUp;
    private readonly InputAction m_Player_MoveDown;
    private readonly InputAction m_Player_Stay;
    private readonly InputAction m_Player_MoveVertical;
    private readonly InputAction m_Player_MoveHorizontal;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @Sneak => m_Wrapper.m_Player_Sneak;
        public InputAction @Stand => m_Wrapper.m_Player_Stand;
        public InputAction @Run => m_Wrapper.m_Player_Run;
        public InputAction @Go => m_Wrapper.m_Player_Go;
        public InputAction @MoveRight => m_Wrapper.m_Player_MoveRight;
        public InputAction @MoveLeft => m_Wrapper.m_Player_MoveLeft;
        public InputAction @MoveUp => m_Wrapper.m_Player_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_Player_MoveDown;
        public InputAction @Stay => m_Wrapper.m_Player_Stay;
        public InputAction @MoveVertical => m_Wrapper.m_Player_MoveVertical;
        public InputAction @MoveHorizontal => m_Wrapper.m_Player_MoveHorizontal;
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
                @Stand.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStand;
                @Stand.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStand;
                @Stand.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStand;
                @Run.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRun;
                @Go.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @Go.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @Go.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGo;
                @MoveRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveRight;
                @MoveLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveLeft;
                @MoveUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveUp;
                @MoveDown.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveDown;
                @Stay.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStay;
                @Stay.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStay;
                @Stay.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStay;
                @MoveVertical.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveVertical;
                @MoveHorizontal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoveHorizontal;
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
                @Stand.started += instance.OnStand;
                @Stand.performed += instance.OnStand;
                @Stand.canceled += instance.OnStand;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Go.started += instance.OnGo;
                @Go.performed += instance.OnGo;
                @Go.canceled += instance.OnGo;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
                @Stay.started += instance.OnStay;
                @Stay.performed += instance.OnStay;
                @Stay.canceled += instance.OnStay;
                @MoveVertical.started += instance.OnMoveVertical;
                @MoveVertical.performed += instance.OnMoveVertical;
                @MoveVertical.canceled += instance.OnMoveVertical;
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
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
    private readonly InputAction m_Weapon_Aim;
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
        public InputAction @Aim => m_Wrapper.m_Weapon_Aim;
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
                @Aim.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
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
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
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
        void OnStand(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnGo(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnStay(InputAction.CallbackContext context);
        void OnMoveVertical(InputAction.CallbackContext context);
        void OnMoveHorizontal(InputAction.CallbackContext context);
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
        void OnAim(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnWeaponSettings(InputAction.CallbackContext context);
    }
}
