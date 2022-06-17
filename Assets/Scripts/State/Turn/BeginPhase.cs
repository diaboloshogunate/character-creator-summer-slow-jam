namespace State.Turn
{
    public class BeginPhase : TurnState
    {

        public BeginPhase(Context context) : base(context) { }

        public override void Enter(TurnState previous)
        {
            Context.OurUnits.ForEach(entity => entity.Refresh());
        }
    }
}