using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerSprintState : PlayerMovementState
    {
        private PlayerSprintData sprintData;
        private float startTime;
        private bool keepSprinting;

        public PlayerSprintState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }



        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = sprintData.SpeedModifier;
            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            keepSprinting = false;
        }

        public override void Update()
        {
            base.Update();

            // Don't transition to another state
            if (keepSprinting)
            {
                return;
            }

            // Not enough time has passed to switch states
            if (Time.time < startTime + sprintData.SprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }
        #endregion ISTATE METHODS



        #region MAIN METHODS
        private void StopSprinting()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            stateMachine.ChangeState(stateMachine.RunState);
        }
        #endregion MAIN METHODS



        #region REUSABLE METHODS
        protected override void AddInputActionsCallBack()
        {
            base.AddInputActionsCallBack();

            stateMachine.Player.Input.PlayerActions.Sprint.performed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallBack()
        {
            base.RemoveInputActionsCallBack();

            stateMachine.Player.Input.PlayerActions.Sprint.performed -= OnSprintPerformed;
        }
        #endregion REUSABLE METHODS



        #region INPUT METHODS
        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;
        }
        #endregion INPUT METHODS
    }
}
