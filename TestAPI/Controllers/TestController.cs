using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly WeatherReader _weatherReader;

    public TestController(WeatherReader weatherReader)
    {
        _weatherReader = weatherReader;
    }

    [HttpGet]
    public async Task<string> GetForecast()
    {
        var forecastAsync = await _weatherReader.GetForecastAsync();
        return forecastAsync;
    }
}