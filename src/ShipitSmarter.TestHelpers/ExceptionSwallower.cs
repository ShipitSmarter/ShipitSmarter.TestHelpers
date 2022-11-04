namespace ShipitSmarter.TestHelpers;

public static class ExceptionSwallower
{
    public static async Task Swallow(Func<Task> func)
    {
        try
        {
            await func();
        }
        catch (Exception ex)
        {
            // Swallow
        }
    }
}