using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshinImpactMovementSystem
{
    [RequireComponent(typeof(PlayerInput), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public Rigidbody PlayerRigidbody { get; private set; }
        public Transform MainCameraTransform { get; private set; }
        public PlayerInput Input { get; private set; }

        private PlayerMovementStateMachine playerMovementStateMachine;

        private void Awake()
        {
            PlayerRigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();

            MainCameraTransform = Camera.main.transform;

            playerMovementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void Start()
        {
            playerMovementStateMachine.ChangeState(playerMovementStateMachine.IdleState); // Enters idle state at start of game
        }

        private void Update()
        {
            playerMovementStateMachine.HandleInput();
            playerMovementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            playerMovementStateMachine.PhysicsUpdate();
        }
    }
}
