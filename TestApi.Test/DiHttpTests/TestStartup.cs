using Microsoft.Extensions.DependencyInjection;
using TestAPI;

namespace TestApi.Test;

public class TestStartup
{
    private readonly string _baseAddress;

    public TestStartup(string baseAddress)
    {
        _baseAddress = baseAddress;
    }

    public IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient("WeatherAPI",
            configureClient =>
            {
                configureClient.BaseAddress = new Uri(_baseAddress);
            });

        services.AddTransient<WeatherReader>();

        return services;
    }
}