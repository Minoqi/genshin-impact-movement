using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerGroundState : PlayerMovementState
    {
        // Variables
        private SlopeData slopeData;

        public PlayerGroundState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            slopeData = stateMachine.Player.ColliderUtility.SlopeData;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Float();
        }

        #region MAIN METHODS
        protected void Float()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.Player.ColliderUtility.CapsuleColliderData.Collider.bounds.center;
            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.Player.LayerData.GroundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);
                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

                // Stops from floating on groudns where angle is too high, instead fall/slide
                if (slopeSpeedModifier == 0f)
                {
                    return;
                }

                float distanceToFloatingPoint = stateMachine.Player.ColliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.Player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                {
                    return;
                }

                float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;
                Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

                stateMachine.Player.PlayerRigidbody.AddForce(liftForce, ForceMode.VelocityChange);
            }

        }

        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            float slopeSpeedModifier = movementData.SlopeSpeedAngles.Evaluate(angle);
            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;
            return slopeSpeedModifier;
        }
        #endregion MAIN METHODS



        #region REUSABLE METHODS
        protected override void AddInputActionsCallBack()
        {
            base.AddInputActionsCallBack();

            stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Dash.started += OnDashStarted;
        }

        protected override void RemoveInputActionsCallBack()
        {
            base.RemoveInputActionsCallBack();

            stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
            stateMachine.Player.Input.PlayerActions.Dash.started -= OnDashStarted;
        }

        protected virtual void Move()
        {
            if (stateMachine.ReusableData.ShouldWalk)
            {
                stateMachine.ChangeState(stateMachine.WalkState);
                return;
            }
            else
            {
                stateMachine.ChangeState(stateMachine.RunState);
            }
        }
        #endregion REUSABLE METHODS



        #region INPUT METHODS
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.DashState);
        }
        #endregion INPUT METHODS
    }
}
