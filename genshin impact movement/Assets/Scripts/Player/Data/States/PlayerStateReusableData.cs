using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public class PlayerStateReusableData
    {
        public Vector2 MovementInput { get; set; }

        public float MovementSpeedModifier { get; set; } = 1f;
        public float MovementOnSlopesSpeedModifier { get; set; } = 1f;
        public bool ShouldWalk { get; set; }

        private Vector3 currentTargetRotation;

        public ref Vector3 CurrentTargetRotation
        {
            get
            {
                return ref currentTargetRotation;
            }
        }

        private Vector3 timeToReachTargetRotation;

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }

        private Vector3 dampedTargetRotationCurrentVelocity;

        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }

        private Vector3 dampedTargetRotationPassedTime;

        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }

        public PlayerStateRotationData RotationData { get; set; }
    }
}
