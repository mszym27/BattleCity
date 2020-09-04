// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerTank"",
            ""id"": ""37f05ec9-3fbc-43bd-8a8d-fd090fbd3a17"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""54d77ffd-caba-4fed-92e8-e0af4f7c0a23"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrows"",
                    ""id"": ""e666a66f-2303-4b9d-be51-827cb58d1068"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76cc0b16-19d5-4120-823a-532fb35a9205"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e4df8ce0-f420-43ff-b36b-d3d0f9494959"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ad1b4f3f-6107-46db-997e-e4e4e50805f3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""693d0a80-a380-4e33-8fc6-2e68b0624bd3"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerTank
        m_PlayerTank = asset.FindActionMap("PlayerTank", throwIfNotFound: true);
        m_PlayerTank_Move = m_PlayerTank.FindAction("Move", throwIfNotFound: true);
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

    // PlayerTank
    private readonly InputActionMap m_PlayerTank;
    private IPlayerTankActions m_PlayerTankActionsCallbackInterface;
    private readonly InputAction m_PlayerTank_Move;
    public struct PlayerTankActions
    {
        private @Controls m_Wrapper;
        public PlayerTankActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerTank_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerTank; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerTankActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerTankActions instance)
        {
            if (m_Wrapper.m_PlayerTankActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerTankActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerTankActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerTankActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerTankActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerTankActions @PlayerTank => new PlayerTankActions(this);
    public interface IPlayerTankActions
    {
        void OnMove(InputAction.CallbackContext context);
    }
}
