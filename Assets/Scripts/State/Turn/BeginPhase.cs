using State.Turn.Player;
using SpawnPhase = State.Turn.AI.SpawnPhase;

namespace State.Turn
{
    public class BeginPhase : TurnState
    {
        public BeginPhase(Context context) : base(context) { }

        public override void Enter(TurnState previous)
        {
            Context.OurUnits.ForEach(entity => entity.Refresh());
            if (Context.IsPlayersTurn)
                Context.StateMachine.Transition(new DrawPhase(Context));
            else
                Context.StateMachine.Transition(new SpawnPhase(Context));
        }
    }
}