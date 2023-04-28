using System.Net;
using Microsoft.Extensions.DependencyInjection;
using TestAPI;

namespace TestApi.Test.DiHttpTests;

public class WeatherReaderDependencyInjectionTest : TestBase
{
    public WeatherReaderDependencyInjectionTest() : base("https://localhost:7140")
    {
    }

    [Fact]
    public async Task GetForecast_200()
    {
        // Arrange
        SetResponseFunc((request, cancellationToken) =>
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("Hello, World!")
            };
            return Task.FromResult(response);
        });

        var expectedData = "Hello, World!";
        var myApiService = ServiceProvider.GetRequiredService<WeatherReader>();

        // Act
        var result = await myApiService.GetForecastAsync();

        // Assert
        Assert.Equal(expectedData, result);
    }

    [Fact]
    public async Task GetForecast_404()
    {
        // Arrange
        SetResponseFunc((request, cancellationToken) =>
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };
            return Task.FromResult(response);
        });

        var myApiService = ServiceProvider.GetRequiredService<WeatherReader>();

        await Assert.ThrowsAsync<HttpRequestException>(() => myApiService.GetForecastAsync());
    }
}