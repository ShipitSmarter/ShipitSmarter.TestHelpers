namespace ShipitSmarter.TestHelpers;

/// <summary>
/// Class that imitates a HttpClient
/// </summary>
public class TestHttpClientFactory : IHttpClientFactory
{
    private readonly TestHttpMessageHandler _httpMessageHandler;

    /// <summary>
    /// Instantiates an instance of TestHttpClientFactory<see cref="TestHttpClientFactory"/>
    /// </summary>
    /// <param name="httpMessageHandler">Instance of a TestHttpMessageHandler <see cref="TestHttpMessageHandler"/></param>
    public TestHttpClientFactory(TestHttpMessageHandler httpMessageHandler)
    {
        _httpMessageHandler = httpMessageHandler;
    }

    /// <summary>
    /// Creates a HttpClient
    /// </summary>
    /// <param name="name">Name of the http client</param>
    /// <returns>An instance of HttpClient with the provided constructor message handler</returns>
    /// <remarks>This doesn't need to be directly called. When used with the WebApplicationFactory, this will be called in the 'AddHttpClient' function in your DI class</remarks>
    public HttpClient CreateClient(string name)
    {
        return new HttpClient(_httpMessageHandler);
    }
}