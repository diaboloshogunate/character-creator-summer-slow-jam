namespace DefaultNamespace
{
    public abstract class GameState
    {
        public abstract GameStates Name { get; }
        public virtual bool CanTransition(GameState previous, GameManager manager) => true;
        public abstract void Enter(GameState previous, GameManager manager);
        public abstract void Exit(GameState next, GameManager manager);
    }
}