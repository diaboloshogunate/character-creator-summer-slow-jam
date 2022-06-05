namespace DefaultNamespace
{
    public class EntityStats
    {
        public Stat Health { get; private set; } = new Stat();
        public Stat Movement { get; private set; } = new Stat();
        public Stat Attack { get; private set; } = new Stat();
        public Stat Defence { get; private set; } = new Stat();
    }
}