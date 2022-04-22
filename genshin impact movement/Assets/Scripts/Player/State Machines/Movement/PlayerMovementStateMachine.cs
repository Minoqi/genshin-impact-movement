namespace GenshinImpactMovementSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player Player { get; }
        public PlayerStateReusableData ReusableData { get; }

        public PlayerIdleState IdleState { get; }
        public PlayerWalkState WalkState { get; }
        public PlayerRunState RunState { get; }
        public PlayerSprintState SprintState { get; }

        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            ReusableData = new PlayerStateReusableData();

            IdleState = new PlayerIdleState(this);
            WalkState = new PlayerWalkState(this);
            RunState = new PlayerRunState(this);
            SprintState = new PlayerSprintState(this);
        }
    }
}
