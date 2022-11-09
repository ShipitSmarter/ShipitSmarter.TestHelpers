namespace ShipitSmarter.TestHelpers;


/// <summary>
/// Helper class to ignore exceptions that are thrown during testing
/// </summary>
public static class ExceptionSwallower
{
    /// <summary>
    /// Ignores any exception that will be thrown
    /// </summary>
    /// <param name="func">Delegate to the function that you are testing</param>
    /// <remarks>This is merely a utility function. It's usage is specifically when you're testing that something has been called (like using .Verify) 
    /// but don't need to configure the rest of the SUT.</remarks>
    public static async Task Swallow(Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (Exception)
        {
            // Swallow
        }
    }
}