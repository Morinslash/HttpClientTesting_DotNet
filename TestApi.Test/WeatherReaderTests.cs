using System.Net;
using Moq;
using Moq.Protected;
using TestAPI;

namespace TestApi.Test;

public class WeatherReaderTests
{
    [Fact]
    public async Task TestWithMoq()
    {
        var expectedData = "Hello, World!";
        
        var messageHandlerMock = new Mock<HttpMessageHandler>();
        messageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(), 
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedData)
            });
        
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(messageHandlerMock.Object));

        var weatherReader = new WeatherReader(httpClientFactoryMock.Object);

        var apiClientResult = await weatherReader.GetForecastAsync();
        
        Assert.Equal(expectedData, apiClientResult);
    }
    
    [Fact]
    public async Task GetDataAsync_ReturnsData()
    {
        var expectedData = "Hello, World!";
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(expectedData)
        };

        var testMessageHandler = new TestHttpMessageHandler(response);
        var testHttpClientFactory = new TestHttpClientFactory(testMessageHandler);
        var myApiService = new WeatherReader(testHttpClientFactory);
        
        var result = await myApiService.GetForecastAsync();
        
        Assert.Equal(expectedData, result);
    }
}