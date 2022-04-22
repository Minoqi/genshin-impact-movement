using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshinImpactMovementSystem
{
    public class PlayerWalkState : PlayerGroundState
    {
        public PlayerWalkState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            speedModifier = 0.225f;
        }
        #endregion ISTATE METHODS



        #region INPUT METHODS
        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.RunState);
        }
        #endregion INPUT METHODS
    }
}
