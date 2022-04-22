using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerRunState : PlayerGroundState
    {
        public PlayerRunState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            speedModifier = 1f;
        }
        #endregion ISTATE METHODS



        #region INPUT METHODS
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.WalkState);
        }
        #endregion INPUT METHODS
    }
}
