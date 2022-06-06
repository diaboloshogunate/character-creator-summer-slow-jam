namespace DefaultNamespace
{
    public class PlayerBeginState : GameState
    {
        public override GameStates Name { get; }

        public override bool CanTransition(GameState previous, GameManager manager) => previous.Name == GameStates.ENEMY_END;

        public override void Enter(GameState previous, GameManager manager)
        {
        }

        public override void Exit(GameState next, GameManager manager)
        {
        }
    }
}