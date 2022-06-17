using System.Collections.Generic;
using DefaultNamespace;

namespace State.Turn
{
    public class Context
    {
        public List<Entity> OurUnits { get; private set; }
        public bool IsPlayersTurn { get; private set; }

        public Context(bool isPlayersTurn, List<Entity> ourUnits)
        {
            IsPlayersTurn = isPlayersTurn;
            OurUnits      = ourUnits;
        }
    }
}