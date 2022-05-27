namespace Imato.Blazor.State
{
    public interface IState<T>
    {
        T? Value { get; set; }
        bool Initialized { get; }

        void StateChanged();
    }
}