namespace State.Turn
{
    public class TurnStateMachine : IStateMachine<TurnState>
    {
        public TurnState State { get; private set; }

        public void Transition(TurnState state)
        {
            if (State != null && !state.CanTransition(State)) return;
            
            state.Enter(State);
            State?.Exit(state);
            State = state;
        }
    }
}