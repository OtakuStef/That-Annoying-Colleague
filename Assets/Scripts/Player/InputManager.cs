using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace UnityTutorial.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput PlayerInput;

        public Vector2 Move {get; private set;}
        public Vector2 Look {get; private set;}
        public bool Run {get; private set;}
        public bool Jump {get; private set;}
        public bool Crouch {get; private set;}
        public float PickUp { get; private set; }
        public bool ThrowObject { get; private set; }
        public float PumpPower { get; private set; }

        private InputActionMap _currentMap;
        private InputAction _moveAction;
        private InputAction _lookAction;
        private InputAction _runAction;
        private InputAction _jumpAction;
        private InputAction _crouchAction;
        private InputAction _pickUpAction;
        private InputAction _throwObjectAction;
        private InputAction _pumpPowerAction;

        private void Awake() {
            HideCursor();
            _currentMap = PlayerInput.currentActionMap;
            _moveAction = _currentMap.FindAction("Move");
            _lookAction = _currentMap.FindAction("Look");
            _runAction  = _currentMap.FindAction("Run");
            _jumpAction = _currentMap.FindAction("Jump");
            _crouchAction = _currentMap.FindAction("Crouch");
            _pickUpAction = _currentMap.FindAction("PickUp");
            _throwObjectAction = _currentMap.FindAction("ThrowObject");
            _pumpPowerAction = _currentMap.FindAction("PumpPower");

            _moveAction.performed += onMove;
            _lookAction.performed += onLook;
            _runAction.performed += onRun;
            _jumpAction.performed += onJump;
            _crouchAction.started += onCrouch;
            _pickUpAction.started += onPickUp;
            _throwObjectAction.started += onThrowObject;
            _pumpPowerAction.started += onPumpPower;

            _moveAction.canceled += onMove;
            _lookAction.canceled += onLook;
            _runAction.canceled += onRun;
            _jumpAction.canceled += onJump;
            _crouchAction.canceled += onCrouch;
            _pickUpAction.canceled += onPickUp;
            _throwObjectAction.canceled += onThrowObject;
            _pumpPowerAction.canceled += onPumpPower;
        }

        private void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }
        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }
        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }
        private void onJump(InputAction.CallbackContext context)
        {
            Jump = context.ReadValueAsButton();
        }
        private void onCrouch(InputAction.CallbackContext context)
        {
            Crouch = context.ReadValueAsButton();
        }
        private void onPickUp(InputAction.CallbackContext context)
        {
            PickUp = context.ReadValue<float>();
        }
        private void onThrowObject (InputAction.CallbackContext context)
        {
            ThrowObject = context.action.WasPressedThisFrame();
        }
        private void onPumpPower(InputAction.CallbackContext context)
        {
            PumpPower = context.ReadValue<float>();
        }



        private void OnEnable() {
            _currentMap.Enable();
        }

        private void OnDisable() {
            _currentMap.Disable();
        }
        
    }
}
