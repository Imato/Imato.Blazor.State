namespace Imato.Blazor.State
{
    public abstract class InvokedState
    {
        private bool initialized;
        public EventHandler OnStateChanged { get; set; } = delegate { };

        protected abstract Task Initialize();

        public bool Initialized => initialized;

        internal async Task Load()
        {
            if (!initialized)
            {
                await Initialize();
                initialized = true;
            }
        }

        public void StateChanged()
        {
            OnStateChanged(this, EventArgs.Empty);
        }
    }
}