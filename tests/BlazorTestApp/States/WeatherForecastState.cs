using BlazorTestApp.Model;
using Imato.Blazor.State;
using System.Net.Http.Json;

namespace BlazorTestApp.States
{
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
}