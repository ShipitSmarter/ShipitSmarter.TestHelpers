# ShipitSmarter TestHelpers

# Http Client

This package provides a set of functionality that allows you to fake any http calls or a chain of http calls.

The most frequently used is the [Test Http Message Handler](./TestHttpMessageHandler.cs) which allows you to fake responses and get the request made.

It contains two methods of 'SetResponse' and 'PopRequest'

These work by using queues and stacks to add a response to the next http request and get the last request for asserting.
This was specifically chosen so that we can fake multiple http requests for integration testing or multiple calls in a loop.

E.G.
```csharp
    public class LogicClass
    {
        private readonly HttpClient _client;
        public LogicClass(HttpClient client)
        {
            _client = client;
        }
        
        public async Task MakeRequest(string content)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "someUrl");
            message.Content = new StringContent(content);
            var response = await _client.SendAsync(message);
            if(response.IsSuccessStatusCode)
            {
                // do something
            }
            else
            {
                // do something else
            }
        }
    }
    
    public class LogicTestClass
    {
        [Fact]
        public async Task WhenContentIsX_ShouldCallSomeUrlWithContentXAndDoSomething()
        {
            // Arrange
            var handler = new TestHttpMessageHandler();
            var client = new HttpClient(handler);
            handler.SetResponse(new HttpResponseMessage(HttpStatusCode.OK));
            
            // Act 
            
            new LogicClass(client).MakeRequest("x");
            
            // Assert
            var lastRequest = handler.PopRequest();
            Assert.Equal(lastRequest.BaseUrl, "someUrl");
            Assert.Equal(lastRequest.Content.ReadAsString(), "x");
        }
    }
```

If your SUT has more requests, you can add more response and also pop more requests from the stack.

If a http request is made that hasn't got an enqueued response, an exception will be thrown

## Integration Testing with WebApplicationFactory

To be able to use the above TestHttpMessageHandler within an integration test. We need to swap out the HttpClient created via DI.

A HttpClient is normally added as a part of the Startup

````csharp
services.AddHttpClient();
````

To replace this, we can use the [Test Http Client Factory](./TestHttpClientFactory.cs)

Override the ConfigureWebHost function in you class derived for the WebApplicationFactory and do:

````csharp

  builder.ConfigureServices(services =>
    {
        var testHttpClientFactory = new TestHttpClientFactory(new TestHttpMessageHandler());
        services.AddSingleton<IHttpClientFactory>(_ => testHttpClientFactory);
    }
````

We can then continue to use TestHttpMessageHandler as we did in the example above

### Service Swapping

[Service Swapper](./ServiceSwapper.cs) provides a simple mechanism to swap out injected dependencies from your startup with a fake dependency.


Program.cs
````csharp
builder.Services.AddSingleton<IMyInterface, MyClass>();
````

FakeMyClass.cs
````csharp

public class FakeMyClass : IMyInterface
{
    
}
````
CustomWebApplicationFactory.cs
````csharp

  builder.ConfigureServices(services =>
    {
        // This will replace the real implementation with the fake implementation.
        // We can also use mocks here
        services.Swap(_ => new FakeMyClass());  
    }
````