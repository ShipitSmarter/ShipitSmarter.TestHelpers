
using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;

namespace ShipitSmarter.TestHelpers;

/// <summary>
/// A static class that provides some helpful extensions and functions around the 'Moq' library
/// </summary>
public static class MockExtensions
{
    /// <summary>
    /// Provides a mock of an injected IOptions as well as the actual dependant data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Tuple of T and IOptionsT</returns>
    public static (T Model, IOptions<T> Options) MockOptions<T>() where T : class
    {
        var fixture = new Fixture();
        var settingsMock = new Mock<IOptions<T>>();
        var item = fixture.Create<T>();
        settingsMock.Setup(t => t.Value).Returns(item);
        return (item, settingsMock.Object);
    }
}