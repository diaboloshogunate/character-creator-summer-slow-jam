namespace State.Turn
{
    public class ActionPhase : TurnState
    {
        public ActionPhase(Context context) : base(context) {}

        public override void Enter(TurnState previous)
        {
            Context.OurUnits.ForEach(entity =>
            {
                entity.Move(entity.DefaultTarget);
            });
        }
    }
}