namespace State
{
    public interface IStateMachine<T> where T : IState<T>
    {
        public T State { get; }
        public void Transition(T state);
    }
}