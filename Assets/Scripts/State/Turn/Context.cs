using System.Collections.Generic;
using DefaultNamespace;

namespace State.Turn
{
    public class Context
    {
        public List<Entity> OurUnits { get; private set; }

        public Context(List<Entity> ourUnits)
        {
            OurUnits = ourUnits;
        }
    }
}