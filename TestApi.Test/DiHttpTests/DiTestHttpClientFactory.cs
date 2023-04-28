namespace TestApi.Test.DiHttpTests;

public class DiTestHttpClientFactory : IHttpClientFactory
{
    private readonly HttpMessageHandler _messageHandler;
    private readonly string _baseAddress;

    public DiTestHttpClientFactory(HttpMessageHandler messageHandler, string baseAddress)
    {
        _messageHandler = messageHandler;
        _baseAddress = baseAddress;
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_messageHandler)
        {
            BaseAddress = new Uri(_baseAddress)
        };
    }
}