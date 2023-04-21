namespace TestApi.Test;

public class TestHttpClientFactory : IHttpClientFactory
{
    private readonly HttpMessageHandler _messageHandler;

    public TestHttpClientFactory(HttpMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_messageHandler);
    }
}