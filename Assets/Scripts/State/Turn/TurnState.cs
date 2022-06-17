namespace State.Turn
{
    public abstract class TurnState : IState<TurnState>
    {
        public Context Context { get; }

        protected TurnState(Context context) => Context = context;

        public virtual bool CanTransition(TurnState state) => true;

        public virtual void Enter(TurnState previous) {}

        public virtual void Exit(TurnState next) {}
    }
}