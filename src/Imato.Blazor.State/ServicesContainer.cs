using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Imato.Blazor.State
{
    public static class ServicesContainer
    {
        private static IServiceProvider? provider;

        public static IServiceProvider Provider
        {
            get
            {
                return provider ?? throw new InvalidOperationException();
            }
            set
            {
                if (provider == null)
                {
                    provider = value;
                }
            }
        }

        private static T? Get<T>()
        {
            if (provider != null)
            {
                var scope = provider.CreateScope();
                return scope.ServiceProvider.GetRequiredService<T>();
            }

            return default;
        }

        public static T GetService<T>()
        {
            var s = Get<T>();
            if (s != null) return s;
            throw new TypeAccessException($"Not registered in DI service {typeof(T).Name}");
        }

        public static State<T> GetState<T>()
        {
            var s = Get<IState<T>>() as State<T>;
            if (s != null) return s;
            throw new TypeAccessException($"Not registered in DI state of {typeof(T).Name}");
        }

        public static WebAssemblyHostBuilder? AddStates<TApp>(this WebAssemblyHostBuilder? builder)
        {
            if (builder != null)
            {
                var assembly = typeof(TApp).Assembly;
                foreach (var t in assembly.GetTypes()
                    .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.Contains("State")))
                {
                    foreach (var i in t.GetInterfaces())
                    {
                        if (i.IsGenericType && i.Name == "IState`1")
                        {
                            builder.Services.AddSingleton(i, t);
                            builder.Services.AddSingleton(t);
                        }
                    }
                }
            }
            return builder;
        }

        public static void Register(WebAssemblyHost? app)
        {
            if (app != null)
            {
                Provider = app.Services;
            }
        }
    }
}