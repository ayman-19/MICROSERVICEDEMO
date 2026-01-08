namespace Shared.Logging;

public static class LoggingExtensions
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure()
    {
        return (context, logger) =>
        {
            var env = context.HostingEnvironment;
            var config = context.Configuration;

            var options = config.GetSection("LoggingOptions").Get<LoggingOptions>() ?? new();

            logger
                .MinimumLevel.Is(ParseLevel(options.LogLevel))
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Environment", env.EnvironmentName)
                .Enrich.WithProperty("Application", options.ApplicationName)
                .WriteTo.Console();

            if (env.IsDevelopment())
            {
                logger.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
                logger.MinimumLevel.Override("Basket", LogEventLevel.Debug);
                logger.MinimumLevel.Override("Ordering", LogEventLevel.Debug);
                logger.MinimumLevel.Override("Discount", LogEventLevel.Debug);
            }

            ConfigureSinks(logger, env, options);
        };
    }

    private static void ConfigureSinks(
        LoggerConfiguration logger,
        IHostEnvironment env,
        LoggingOptions options
    )
    {
        //// File sink (always)
        //logger.WriteTo.File(
        //    path: "logs/log-.txt",
        //    rollingInterval: RollingInterval.Day,
        //    retainedFileCountLimit: 14,
        //    restrictedToMinimumLevel: LogEventLevel.Information
        //);

        //// Seq
        //if (!string.IsNullOrWhiteSpace(options.SeqUrl))
        //{
        //    logger.WriteTo.Seq(options.SeqUrl);
        //}

        // Elastic
        if (!string.IsNullOrWhiteSpace(options.ElasticUrl))
        {
            logger.WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(options.ElasticUrl))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                    IndexFormat =
                        $"{options.ApplicationName.ToLower()}-{env.EnvironmentName.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                    MinimumLogEventLevel = LogEventLevel.Debug,
                }
            );
        }
    }

    private static LogEventLevel ParseLevel(string level) =>
        Enum.TryParse(level, true, out LogEventLevel parsed) ? parsed : LogEventLevel.Information;
}
