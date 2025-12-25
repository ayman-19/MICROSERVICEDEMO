namespace Discount.Infrastructure.Extensions;

public static class DiscountDbExtension
{
    private const int MaxRetryCount = 5;
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(2);

    public static IHost MigrateDatabase<TContext>(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        try
        {
            logger.LogInformation("Starting PostgreSQL database migration.");

            ApplyMigration(configuration, logger);

            logger.LogInformation("PostgreSQL database migration completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }

        return host;
    }

    private static void ApplyMigration(IConfiguration configuration, ILogger logger)
    {
        var connectionString = configuration.GetValue<string>("PostgreSettings:Connection");

        using var connection = new NpgsqlConnection(connectionString);

        Helpers.RetryPolicy.Execute(
            MaxRetryCount,
            RetryDelay,
            logger,
            () => ExecuteMigration(connection)
        );
    }

    private static void ExecuteMigration(NpgsqlConnection connection)
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS Coupon (
                Id SERIAL PRIMARY KEY,
                ProductName VARCHAR(24) NOT NULL UNIQUE,
                Description TEXT,
                Amount DECIMAL(10,2)
            );
            """;

        connection.Open();

        using var command = new NpgsqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }
}
