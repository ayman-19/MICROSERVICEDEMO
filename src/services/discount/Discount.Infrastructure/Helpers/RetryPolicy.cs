namespace Discount.Infrastructure.Helpers;

public static class RetryPolicy
{
    public static void Execute(int retryCount, TimeSpan delay, ILogger logger, Action action)
    {
        for (var attempt = 1; attempt <= retryCount; attempt++)
        {
            try
            {
                action();
                return;
            }
            catch (NpgsqlException ex) when (attempt < retryCount)
            {
                logger.LogWarning(
                    ex,
                    "Database migration failed. Retrying attempt {Attempt}/{RetryCount}...",
                    attempt,
                    retryCount
                );

                Thread.Sleep(delay);
            }
        }
    }
}
