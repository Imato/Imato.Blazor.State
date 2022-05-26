namespace Imato.Blazor.State
{
    public class NotInitializedException : Exception
    {
        public NotInitializedException() : base("State not initialized")
        {
        }

        public NotInitializedException(Type t) : base($"State of {t.Name} not initialized")
        {
        }
    }
}