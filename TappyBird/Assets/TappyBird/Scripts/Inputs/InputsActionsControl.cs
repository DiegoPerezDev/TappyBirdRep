// GENERATED AUTOMATICALLY FROM 'Assets/TappyBird/Scripts/Inputs/InputsActionsControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputsActionsControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputsActionsControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputsActionsControl"",
    ""maps"": [
        {
            ""name"": ""PlayingInputs"",
            ""id"": ""450e91b1-833a-41bc-864d-2e14baffd795"",
            ""actions"": [
                {
                    ""name"": ""jump"",
                    ""type"": ""Button"",
                    ""id"": ""6d9229cd-97df-44cd-9550-1a71a519580f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""6f6be2ad-e187-4aa5-ad27-70d2d0792c26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""tap"",
                    ""type"": ""Value"",
                    ""id"": ""b439755e-9eb6-4830-beaa-efcfade6fe6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""fingerPos"",
                    ""type"": ""Value"",
                    ""id"": ""0045cf54-d269-4410-8357-9695a706f905"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ed09d3dc-3ffb-485f-8666-487b7b87898e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""pc"",
                    ""action"": ""jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55caf9a6-3cfb-4972-9a8c-b6e6b495cdd3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""pc"",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44d5c5d6-3061-4e50-b649-f96968dcb7fe"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""pc"",
                    ""action"": ""tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc884039-9712-4420-b1b3-3cc88b3e8f48"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""smartphone"",
                    ""action"": ""tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47725726-7d48-425f-a41b-79b79407e94e"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""smartphone"",
                    ""action"": ""fingerPos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MenuInputs"",
            ""id"": ""17bfb434-9b46-4d4f-aecb-1288e6f1277c"",
            ""actions"": [
                {
                    ""name"": ""back"",
                    ""type"": ""Button"",
                    ""id"": ""56618e7b-0c8b-4c23-b5eb-7da769bb8c6a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2ba5883-a06e-4c85-86a9-18a83673970e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""pc"",
                    ""action"": ""back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""pc"",
            ""bindingGroup"": ""pc"",
            ""devices"": []
        },
        {
            ""name"": ""smartphone"",
            ""bindingGroup"": ""smartphone"",
            ""devices"": []
        }
    ]
}");
        // PlayingInputs
        m_PlayingInputs = asset.FindActionMap("PlayingInputs", throwIfNotFound: true);
        m_PlayingInputs_jump = m_PlayingInputs.FindAction("jump", throwIfNotFound: true);
        m_PlayingInputs_pause = m_PlayingInputs.FindAction("pause", throwIfNotFound: true);
        m_PlayingInputs_tap = m_PlayingInputs.FindAction("tap", throwIfNotFound: true);
        m_PlayingInputs_fingerPos = m_PlayingInputs.FindAction("fingerPos", throwIfNotFound: true);
        // MenuInputs
        m_MenuInputs = asset.FindActionMap("MenuInputs", throwIfNotFound: true);
        m_MenuInputs_back = m_MenuInputs.FindAction("back", throwIfNotFound: true);
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

    // PlayingInputs
    private readonly InputActionMap m_PlayingInputs;
    private IPlayingInputsActions m_PlayingInputsActionsCallbackInterface;
    private readonly InputAction m_PlayingInputs_jump;
    private readonly InputAction m_PlayingInputs_pause;
    private readonly InputAction m_PlayingInputs_tap;
    private readonly InputAction m_PlayingInputs_fingerPos;
    public struct PlayingInputsActions
    {
        private @InputsActionsControl m_Wrapper;
        public PlayingInputsActions(@InputsActionsControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @jump => m_Wrapper.m_PlayingInputs_jump;
        public InputAction @pause => m_Wrapper.m_PlayingInputs_pause;
        public InputAction @tap => m_Wrapper.m_PlayingInputs_tap;
        public InputAction @fingerPos => m_Wrapper.m_PlayingInputs_fingerPos;
        public InputActionMap Get() { return m_Wrapper.m_PlayingInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayingInputsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayingInputsActions instance)
        {
            if (m_Wrapper.m_PlayingInputsActionsCallbackInterface != null)
            {
                @jump.started -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnJump;
                @jump.performed -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnJump;
                @jump.canceled -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnJump;
                @pause.started -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnPause;
                @tap.started -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnTap;
                @tap.performed -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnTap;
                @tap.canceled -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnTap;
                @fingerPos.started -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnFingerPos;
                @fingerPos.performed -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnFingerPos;
                @fingerPos.canceled -= m_Wrapper.m_PlayingInputsActionsCallbackInterface.OnFingerPos;
            }
            m_Wrapper.m_PlayingInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @jump.started += instance.OnJump;
                @jump.performed += instance.OnJump;
                @jump.canceled += instance.OnJump;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
                @tap.started += instance.OnTap;
                @tap.performed += instance.OnTap;
                @tap.canceled += instance.OnTap;
                @fingerPos.started += instance.OnFingerPos;
                @fingerPos.performed += instance.OnFingerPos;
                @fingerPos.canceled += instance.OnFingerPos;
            }
        }
    }
    public PlayingInputsActions @PlayingInputs => new PlayingInputsActions(this);

    // MenuInputs
    private readonly InputActionMap m_MenuInputs;
    private IMenuInputsActions m_MenuInputsActionsCallbackInterface;
    private readonly InputAction m_MenuInputs_back;
    public struct MenuInputsActions
    {
        private @InputsActionsControl m_Wrapper;
        public MenuInputsActions(@InputsActionsControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @back => m_Wrapper.m_MenuInputs_back;
        public InputActionMap Get() { return m_Wrapper.m_MenuInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuInputsActions set) { return set.Get(); }
        public void SetCallbacks(IMenuInputsActions instance)
        {
            if (m_Wrapper.m_MenuInputsActionsCallbackInterface != null)
            {
                @back.started -= m_Wrapper.m_MenuInputsActionsCallbackInterface.OnBack;
                @back.performed -= m_Wrapper.m_MenuInputsActionsCallbackInterface.OnBack;
                @back.canceled -= m_Wrapper.m_MenuInputsActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_MenuInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @back.started += instance.OnBack;
                @back.performed += instance.OnBack;
                @back.canceled += instance.OnBack;
            }
        }
    }
    public MenuInputsActions @MenuInputs => new MenuInputsActions(this);
    private int m_pcSchemeIndex = -1;
    public InputControlScheme pcScheme
    {
        get
        {
            if (m_pcSchemeIndex == -1) m_pcSchemeIndex = asset.FindControlSchemeIndex("pc");
            return asset.controlSchemes[m_pcSchemeIndex];
        }
    }
    private int m_smartphoneSchemeIndex = -1;
    public InputControlScheme smartphoneScheme
    {
        get
        {
            if (m_smartphoneSchemeIndex == -1) m_smartphoneSchemeIndex = asset.FindControlSchemeIndex("smartphone");
            return asset.controlSchemes[m_smartphoneSchemeIndex];
        }
    }
    public interface IPlayingInputsActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
        void OnFingerPos(InputAction.CallbackContext context);
    }
    public interface IMenuInputsActions
    {
        void OnBack(InputAction.CallbackContext context);
    }
}
