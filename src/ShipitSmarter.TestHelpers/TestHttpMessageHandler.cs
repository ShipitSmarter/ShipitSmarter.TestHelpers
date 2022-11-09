using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ShipitSmarter.TestHelpers;

/// <summary>
/// Class that takes the place of a HttpClient MessageHandler specifically for mocking Http Requests
/// </summary>
public class TestHttpMessageHandler : HttpMessageHandler
{
    private readonly Queue<HttpResponseMessage> _responses = new();
    private readonly Stack<HttpRequestMessage> _completedRequests = new();

    /// <summary>
    /// Enqueues a response for the next http call
    /// </summary>
    /// <param name="response">The response that you want returned from the HttpClient</param>
    public void SetResponse(HttpResponseMessage response)
    {
        _responses.Enqueue(response);
    }

    /// <summary>
    /// Returns the last request made via a http client
    /// </summary>
    /// <returns>The last request made by the Http Client</returns>
    public HttpRequestMessage PopRequest()
    {
        return _completedRequests.Pop();
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        _completedRequests.Push(request);
        return Task.FromResult(_responses.Dequeue());
    }
}