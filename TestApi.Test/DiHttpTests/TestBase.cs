using System.Net;
using Microsoft.Extensions.DependencyInjection;
using TestApi.Test.DiHttpTests;

namespace TestApi.Test;

public class TestBase
{
    private readonly string _baseAddress;
    protected IServiceProvider ServiceProvider;
    private readonly DiTestHttpMessageHandler _testMessageHandler;
    
    public TestBase(string baseAddress)
    {
        _baseAddress = baseAddress;
        _testMessageHandler = new DiTestHttpMessageHandler();
        var testHttpClientFactory = new DiTestHttpClientFactory(_testMessageHandler, _baseAddress);

        var testStartup = new TestStartup(_baseAddress);
        var services = testStartup.ConfigureServices();

        services.AddSingleton<IHttpClientFactory>(testHttpClientFactory);
        
        ServiceProvider = services.BuildServiceProvider();
    } 
    
    protected void SetResponseFunc(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responseFunc)
    {
        _testMessageHandler.ResponseFunc = responseFunc;
    }
}