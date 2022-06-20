using DefaultNamespace;

namespace State.Turn
{
    public class EndPhase : TurnState
    {
        public EndPhase(Context context) : base(context) {}

        public override void Enter(TurnState previous)
        {
            Context.GameManager.NextTurn(Context);
        }
    }
}