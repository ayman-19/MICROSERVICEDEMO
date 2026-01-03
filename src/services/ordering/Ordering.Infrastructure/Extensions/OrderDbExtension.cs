namespace Ordering.Infrastructure.Extensions;

public static class OrderDbExtension
{
    private const int MaxRetryCount = 5;
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(2);

    public static IHost MigrateDatabase<TContext>(this IHost host)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation("Starting SQL Server database migration.");

            Helpers.RetryPolicy.Execute(
                MaxRetryCount,
                RetryDelay,
                logger,
                () => ExecuteMigration(context)
            );

            logger.LogInformation("SQL Server database migration completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }

        return host;
    }

    private static void ExecuteMigration<TContext>(TContext context)
        where TContext : DbContext
    {
        context.Database.Migrate();
    }
}
