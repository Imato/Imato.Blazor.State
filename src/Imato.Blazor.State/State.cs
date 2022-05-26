namespace Imato.Blazor.State
{
    public abstract class State<T> : InvokedState, IState<T>
    {
        protected T? state;

        public T Value
        {
            get
            {
                return state ?? throw new NotInitializedException();
            }
            set
            {
                state = value;
                StateChanged();
            }
        }
    }
}