using System.Collections.Generic;
using DefaultNamespace;

namespace State.Turn
{
    public class Context
    {
        public TurnStateMachine StateMachine { get; private set; }
        public List<Entity> OurUnits { get; private set; }
        public bool IsPlayersTurn { get; private set; }

        public Context(TurnStateMachine stateMachine, bool isPlayersTurn, List<Entity> ourUnits)
        {
            StateMachine  = stateMachine;
            IsPlayersTurn = isPlayersTurn;
            OurUnits      = ourUnits;
        }
    }
}