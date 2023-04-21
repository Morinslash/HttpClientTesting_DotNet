using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace TestApi.Test;

public class TestBase
{
    protected IServiceProvider ServiceProvider;
    private readonly DiTestHttpMessageHandler _testMessageHandler;
    
    public TestBase()
    {
        _testMessageHandler = new DiTestHttpMessageHandler();
        var testHttpClientFactory = new TestHttpClientFactory(_testMessageHandler);

        var testStartup = new TestStartup();
        var services = testStartup.ConfigureServices();

        services.AddSingleton<IHttpClientFactory>(testHttpClientFactory);
        
        ServiceProvider = services.BuildServiceProvider();
    } 
    
    protected void SetResponseFunc(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responseFunc)
    {
        _testMessageHandler.ResponseFunc = responseFunc;
    }
}