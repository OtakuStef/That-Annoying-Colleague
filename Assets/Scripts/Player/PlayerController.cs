using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThatAnnoyyingColleagueInput.Manager;

namespace ThatAnnoyyingColleagueInput.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField, Range(10, 500)] private float JumpFactor = 260f;
        [SerializeField] private float Dis2Ground = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float AirResistance = 0.8f;
        private Rigidbody playerRigidbody;
        private InputManager inputManager;
        private Animator animator;
        private bool grounded = false;
        private bool hasAnimator;
        private int xVelHash;
        private int yVelHash;
        private int jumpHash;
        private int groundHash;
        private int fallingHash;
        private int zVelHash;
        private int crouchHash;
        private float xRotation;

        private const float walkSpeed = 2f;
        private const float runSpeed = 6f;
        private Vector2 currentVelocity;

        // adding audio manager to add sound effects for player
        AudioManager audioManager;
        private bool isFootstepPlaying = false;
        private bool wasRunning = false;

        //adding powerup boost variables
        private float speedMultiplier = 1f;
        private  bool isShieldActive = false;

        private void Start() {
            hasAnimator = TryGetComponent<Animator>(out animator);
            playerRigidbody = GetComponent<Rigidbody>();
            inputManager = GetComponent<InputManager>();


            xVelHash = Animator.StringToHash("X_Velocity");
            yVelHash = Animator.StringToHash("Y_Velocity");
            zVelHash = Animator.StringToHash("Z_Velocity");
            jumpHash = Animator.StringToHash("Jump");
            groundHash = Animator.StringToHash("Grounded");
            fallingHash = Animator.StringToHash("Falling");
            crouchHash = Animator.StringToHash("Crouch");
        }

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        private void FixedUpdate() {
            SampleGround();
            Move();
            HandleJump();
            HandleCrouch();
        }
        private void LateUpdate() {
            CamMovements();
        }

        private void Move()
        {
            if(!hasAnimator) return;

            float targetSpeed = inputManager.Run ? runSpeed : walkSpeed;
            targetSpeed *= speedMultiplier;

            AudioClip stepsSound = inputManager.Run ? audioManager.steps_running_01 : audioManager.steps_01;
            bool isRunning = inputManager.Run;
            bool isMoving = inputManager.Move != Vector2.zero;

            if (inputManager.Crouch)
            {
                targetSpeed = 1.5f;
                targetSpeed *= speedMultiplier;
            }
            if (inputManager.Move == Vector2.zero)  targetSpeed = 0;
            

            if (grounded)
            {
                
                currentVelocity.x = Mathf.Lerp(currentVelocity.x, inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
                currentVelocity.y =  Mathf.Lerp(currentVelocity.y, inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = currentVelocity.x - playerRigidbody.velocity.x;
                var zVelDifference = currentVelocity.y - playerRigidbody.velocity.z;

                playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0 , zVelDifference)), ForceMode.VelocityChange);

                if (isMoving)
                {
                    if (!isFootstepPlaying || wasRunning != isRunning)
                    {
                        audioManager.StopSFX();
                        audioManager.PlaySFX(stepsSound, true);
                        isFootstepPlaying = true;
                        wasRunning = isRunning;
                    }
                }
                else if (isFootstepPlaying)
                {
                    audioManager.StopSFX();
                    isFootstepPlaying = false;
                }

            }
            else
            {
                playerRigidbody.AddForce(transform.TransformVector(new Vector3(currentVelocity.x * AirResistance,0,currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }


            animator.SetFloat(xVelHash , currentVelocity.x);
            animator.SetFloat(yVelHash, currentVelocity.y);
        }

        private void CamMovements()
        {
            if(!hasAnimator) return;

            var MouseX = inputManager.Look.x;
            var MouseY = inputManager.Look.y;
            Camera.position = CameraRoot.position;
            
            
            xRotation -= MouseY * MouseSensitivity * Time.smoothDeltaTime;
            xRotation = Mathf.Clamp(xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(xRotation, 0 , 0);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0, MouseX * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void HandleCrouch() => animator.SetBool(crouchHash , inputManager.Crouch);


        private void HandleJump()
        {
            if(!hasAnimator) return;
            if(!inputManager.Jump) return;
            if(!grounded) return;
            animator.SetTrigger(jumpHash);

            //Bunny Hop Mechanic
            playerRigidbody.AddForce(-playerRigidbody.velocity.y * Vector3.up, ForceMode.VelocityChange);
            playerRigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            animator.ResetTrigger(jumpHash);
        }

        private void SampleGround()
        {
            if(!hasAnimator) return;
            
            RaycastHit hitInfo;
            if(Physics.Raycast(playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                //Grounded
                grounded = true;
                SetAnimationGrounding();
                return;
            }
            //Falling
            grounded = false;
            animator.SetFloat(zVelHash, playerRigidbody.velocity.y);
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            animator.SetBool(fallingHash, !grounded);
            animator.SetBool(groundHash, grounded);
        }

        //powerup functions
        public void setSpeedMultiplier(float multiplier)
        {
            speedMultiplier = multiplier;
        }

        public void setShield(bool shieldActive)
        {
            isShieldActive = shieldActive;
        }

        public bool getShieldStatus()
        {
            return isShieldActive;
        }

    }
}
