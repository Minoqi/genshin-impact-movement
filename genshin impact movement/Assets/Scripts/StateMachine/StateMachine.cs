namespace GenshinImpactMovementSystem
{
    public abstract class StateMachine
    {
        protected IState currentState;
        public void ChangeState(IState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;

            currentState.Enter();
        }

        public void HandleInput()
        {
            if (currentState != null)
            {
                currentState.HandleInput();
            }
        }

        public void Update()
        {
            if (currentState != null)
            {
                currentState.Update();
            }
        }

        public void PhysicsUpdate()
        {
            if (currentState != null)
            {
                currentState.PhysicsUpdate();
            }
        }
    }
}
