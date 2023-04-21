namespace TestAPI;

public class WeatherReader
{
   private readonly IHttpClientFactory _httpClientFactory;

   public WeatherReader(IHttpClientFactory httpClientFactory)
   {
      _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
   }

   public async Task<string> GetForecastAsync()
   {
      var httpClient = _httpClientFactory.CreateClient();
      httpClient.BaseAddress = new Uri("https://localhost:7140");

      var httpResponseMessage = await httpClient.GetAsync("/WeatherForecast");
      httpResponseMessage.EnsureSuccessStatusCode();
      var readAsStringAsync = await httpResponseMessage.Content.ReadAsStringAsync();
      
      return readAsStringAsync;
   }
}