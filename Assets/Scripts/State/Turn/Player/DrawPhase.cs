namespace State.Turn.Player
{
    public class DrawPhase : TurnState
    {
        public DrawPhase(Context context) : base(context) {}

        public override void Enter(TurnState previous)
        {
            base.Enter(previous);
            Context.StateMachine.Transition(new SpawnPhase(Context));
        }
    }
}