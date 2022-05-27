using Microsoft.AspNetCore.Components;

namespace Imato.Blazor.State
{
    public class StateComponent<T> : StateComponent
    {
        private State<T>? _state;

        protected T? State
        {
            get
            {
                return _state != null ? _state.Value : default;
            }
            set
            {
                if (_state == null) throw new NotInitializedException(typeof(T));
                if (value == null) throw new ArgumentNullException("Value of state cannot be null");
                _state.Value = value;
            }
        }

        public void StateChanged()
        {
            _state?.StateChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            _state = await GetStateOf<T>();
            await base.OnInitializedAsync();
        }
    }

    public class LayoutStateComponent<T> : LayoutStateComponent
    {
        private State<T>? _state;

        protected T? State
        {
            get
            {
                return _state != null ? _state.Value : default;
            }
            set
            {
                if (_state == null) throw new NotInitializedException(typeof(T));
                if (value == null) throw new ArgumentNullException("Value of state cannot be null");
                _state.Value = value;
            }
        }

        public void StateChanged()
        {
            _state?.StateChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            _state = await GetStateOf<T>();
            await base.OnInitializedAsync();
        }
    }

    public class StateComponent : ComponentBase
    {
        protected async Task<State<T>> GetStateOf<T>()
        {
            var state = ServicesContainer.GetState<T>();
            state.OnStateChanged += (_, _) => StateHasChanged();
            await state.Load();
            return state;
        }

        protected async Task<T> GetState<T>()
        {
            var state = ServicesContainer.GetService<T>();
            var ins = state as InvokedState;
            if (ins == null)
            {
                throw new TypeAccessException($"Unknown state {typeof(T).Name}");
            }
            ins.OnStateChanged += (_, _) => StateHasChanged();
            await ins.Load();
            return state;
        }
    }

    public class LayoutStateComponent : LayoutComponentBase
    {
        protected async Task<State<T>> GetStateOf<T>()
        {
            var state = ServicesContainer.GetState<T>();
            state.OnStateChanged += (_, _) => StateHasChanged();
            await state.Load();
            return state;
        }

        protected async Task<T> GetState<T>()
        {
            var state = ServicesContainer.GetService<T>();
            var ins = state as InvokedState;
            if (ins == null)
            {
                throw new TypeAccessException($"Unknown state {typeof(T).Name}");
            }
            ins.OnStateChanged += (_, _) => StateHasChanged();
            await ins.Load();
            return state;
        }
    }
}