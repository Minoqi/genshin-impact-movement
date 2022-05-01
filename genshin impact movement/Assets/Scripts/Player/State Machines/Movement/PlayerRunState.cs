using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerRunState : PlayerMovingState
    {
        private float startTime;
        private PlayerSprintData sprintData;

        public PlayerRunState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.SprintData;
        }

        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;
            startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            // Should be running
            if (!stateMachine.ReusableData.ShouldWalk)
            {
                return;
            }

            // Keep running
            if (Time.time < startTime + sprintData.RunToWalkTime)
            {
                return;
            }

            StopRunning();
        }
        #endregion ISTATE METHODS



        #region MAIN METHODS
        private void StopRunning()
        {
            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            stateMachine.ChangeState(stateMachine.WalkState);
        }
        #endregion MIAN METHODS



        #region INPUT METHODS
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.WalkState);
        }
        #endregion INPUT METHODS
    }
}
