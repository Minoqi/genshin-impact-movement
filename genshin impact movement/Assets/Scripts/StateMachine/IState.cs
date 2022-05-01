using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update(); // non-physics related logic
        public void PhysicsUpdate();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitionEvent();
    }
}
