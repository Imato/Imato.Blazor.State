# Imato.Blazor.State
Simple Blazor state container

Usage  
0. Setup app in Program.cs
```csharp
using Imato.Blazor.State;

builder.Services.AddStates<App>();

var app = builder.Build();
ServicesContainer.Register(app);
await app.RunAsync();
```

1. Create state class
```csharp
using Imato.Blazor.State;

public class WeatherForecastState : State<List<WeatherForecast>>
{
    private readonly HttpClient http;

    public WeatherForecastState(HttpClient http)
    {
        this.http = http;
    }

    protected override async Task Initialize()
    {
        await Reload();
    }

    public async Task Reload()
    {
        Value = await http.GetFromJsonAsync<List<WeatherForecast>>("sample-data/weather.json") ?? new List<WeatherForecast>();
    }
}
```

2. Create razor page or component from using StateComponent then you have to use multiple states in one component. Just add state with GetStateOf<T>() or GetState<TState>()
```csharp
@page "/fetchdata"
@inject HttpClient Http
@inherits StateComponent
...
@if (state == null || !state.Initialized)
{
    <p><em>Loading...</em></p>
}
...
@code {
    State<List<WeatherForecast>>? state;

    protected override async Task OnInitializedAsync()
    {
        state = await GetStateOf<List<WeatherForecast>>();
        // state = await GetState<WeatherForecastState>();
        await base.OnInitializedAsync();
    }

    void AddNew()
    {
        if (state != null)
        {
            ...
            state.Value.Add(n);
            // Replace state.Value on notify about changes state.StateChanged()
            state.StateChanged();
        }
    }
}
```

For one state per component use generic StateComponent<T>
```csharp
@inherits StateComponent<List<WeatherForecast>>

<div>
    Total Forecasts: @State?.Count
</div>
```

