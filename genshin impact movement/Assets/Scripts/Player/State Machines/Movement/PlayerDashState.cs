using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerDashState : PlayerGroundState
    {
        private PlayerDashData dashData;
        private float startTime;
        private int consecutiveDahesUsed;

        public PlayerDashState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            dashData = movementData.DashData;
        }



        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            stateMachine.ReusableData.MovementSpeedModifier = movementData.DashData.SpeedModifier;

            AddForceOnTransitionFromStationaryState();

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.ReusableData.MovementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }

            stateMachine.ChangeState(stateMachine.SprintState);
        }
        #endregion ISTATE METHODS



        #region MAIN METHODS
        private void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.ReusableData.MovementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterRotationDirection = stateMachine.Player.transform.forward;
            characterRotationDirection.y = 0f;
            stateMachine.Player.PlayerRigidbody.velocity = characterRotationDirection * GetMovementSpeed();
        }

        private void UpdateConsecutiveDashes()
        {
            // Reset consecutive dashes
            if (!IsConsecutive())
            {
                consecutiveDahesUsed = 0;
            }

            consecutiveDahesUsed++;

            if (consecutiveDahesUsed == dashData.ConsecutiveDashesLimitAmount)
            {
                consecutiveDahesUsed = 0;

                stateMachine.Player.Input.DisableActionFor(stateMachine.Player.Input.PlayerActions.Dash, dashData.DashLimitReachedCooldown);
            }
        }

        private bool IsConsecutive()
        {
            return Time.time < startTime + dashData.TimeToBeConsideredConsecutive;
        }
        #endregion MAIN METHODS



        #region INPUT METHODS
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
        }
        #endregion INPUT METHODS
    }
}
