using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace ThatAnnoyyingColleagueInput.Manager
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

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction runAction;
        private InputAction jumpAction;
        private InputAction crouchAction;
        private InputAction pickUpAction;
        private InputAction throwObjectAction;
        private InputAction pumpPowerAction;

        private void Awake() {
            HideCursor();
            currentMap = PlayerInput.currentActionMap;
            moveAction = currentMap.FindAction("Move");
            lookAction = currentMap.FindAction("Look");
            runAction  = currentMap.FindAction("Run");
            jumpAction = currentMap.FindAction("Jump");
            crouchAction = currentMap.FindAction("Crouch");
            pickUpAction = currentMap.FindAction("PickUp");
            throwObjectAction = currentMap.FindAction("ThrowObject");
            pumpPowerAction = currentMap.FindAction("PumpPower");

            moveAction.performed += onMove;
            lookAction.performed += onLook;
            runAction.performed += onRun;
            jumpAction.performed += onJump;
            crouchAction.started += onCrouch;
            pickUpAction.started += onPickUp;
            throwObjectAction.started += onThrowObject;
            pumpPowerAction.started += onPumpPower;

            moveAction.canceled += onMove;
            lookAction.canceled += onLook;
            runAction.canceled += onRun;
            jumpAction.canceled += onJump;
            crouchAction.canceled += onCrouch;
            pickUpAction.canceled += onPickUp;
            throwObjectAction.canceled += onThrowObject;
            pumpPowerAction.canceled += onPumpPower;
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
            currentMap.Enable();
        }

        private void OnDisable() {
            currentMap.Disable();
        }
        
    }
}
