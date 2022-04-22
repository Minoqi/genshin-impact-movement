using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerMovementState : IState
    {
        // Variables
        protected PlayerMovementStateMachine stateMachine;
        protected bool shouldWalk;

        protected Vector2 movementInput;
        protected float baseSpeed = 5f;
        protected float speedModifier = 1f;

        // Using vector3 since gliding system needs an x and z
        protected Vector3 currentTargetRotation;
        protected Vector3 timeToReachTargetRotation;
        protected Vector3 dampedTargetRotationCurrentVelocity;
        protected Vector3 dampedTargetRotationPassedTime;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            InitializeData();
        }

        private void InitializeData()
        {
            timeToReachTargetRotation.y = 0.14f; // Tie it takes for rotation to happen in genshin
        }



        #region ISTATE METHODS
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);

            AddInputActionsCallBack();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallBack();
        }

        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void Update()
        {

        }
        #endregion ISTATE METHODS



        #region MAIN METHODS
        private void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            // Not moving
            if (movementInput == Vector2.zero || speedModifier == 0f) // Not moving
            {
                return;
            }

            // Move
            Vector3 movementDirection = GetMovementDirection();
            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed();
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            stateMachine.Player.PlayerRigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);

            // NOTE :: AddForce() happens next update, changing velocity is instantaneous 
            // NOTE :: Vector multiplcation takes resources, so for optimization make sure you always multiply the vector last (ex. float * float * vector)
        }

        private float Rotate(Vector3 direction)
        {
            // Variables
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        #endregion MAIN METHODS



        #region REUSABLE METHODS
        protected Vector3 GetMovementDirection()
        {
            return new Vector3(movementInput.x, 0f, movementInput.y);
        }

        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModifier;
        }

        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.PlayerRigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.PlayerRigidbody.rotation.eulerAngles.y;

            if (currentYAngle == currentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y, timeToReachTargetRotation.y);

            dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.Player.PlayerRigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            // Variables
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // Convert negative degrees to positive
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            // Add camera rotation to angle
            if (shouldConsiderCameraRotation)
            {
                directionAngle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
                if (directionAngle > 360f)
                {
                    directionAngle -= 360;
                }
            }

            if (directionAngle != currentTargetRotation.y)
            {
                currentTargetRotation.y = directionAngle;
                dampedTargetRotationPassedTime.y = 0f;
            }

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // NOTE :: Vector3.forward is in world space
        }

        protected void ResetVelocity()
        {
            stateMachine.Player.PlayerRigidbody.velocity = Vector3.zero;
        }

        protected virtual void AddInputActionsCallBack()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }

        protected virtual void RemoveInputActionsCallBack()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }
        #endregion REUSABLE METHODS



        #region INPUT METHODS
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            shouldWalk = !shouldWalk;
        }
        #endregion INPUT METHODS
    }
}
