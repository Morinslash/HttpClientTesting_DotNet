using Microsoft.Extensions.DependencyInjection;
using TestAPI;

namespace TestApi.Test;

public class TestStartup
{
    public IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient("WeatherAPI",
            configureClient =>
            {
                configureClient.BaseAddress = new Uri("https://localhost:7140");
            });

        services.AddTransient<WeatherReader>();

        return services;
    }
}