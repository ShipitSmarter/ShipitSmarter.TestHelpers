using System.Net.Http;

namespace ShipitSmarter.TestHelpers;

public class TestHttpClientFactory
{
    private readonly TestHttpMessageHandler _httpMessageHandler;

    public TestHttpClientFactory(TestHttpMessageHandler httpMessageHandler)
    {
        _httpMessageHandler = httpMessageHandler;
    }

    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_httpMessageHandler);
    }
}