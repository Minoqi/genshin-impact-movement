using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }



        #region ISTATE METHODS
        public override void Enter()
        {
            base.Enter();

            speedModifier = 0f; // Stop player from moving
            ResetVelocity(); // Stop physics
        }

        public override void Update()
        {
            base.Update();

            if (movementInput == Vector2.zero)
            {
                return;
            }

            Move();
        }
        #endregion ISTATE METHODS
    }
}
