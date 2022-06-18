namespace State
{
    public interface IState<T> where T : IState<T>
    {
        public bool CanTransition(T state);
        public void Enter(T previous);
        public void Exit(T next);
    }
}