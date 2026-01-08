namespace Shared.Logging;

public sealed class LoggingOptions
{
    public string ApplicationName { get; set; } = default!;
    public string Environment { get; set; } = default!;
    public string SeqUrl { get; set; } = string.Empty;
    public string ElasticUrl { get; set; } = string.Empty;
    public string LogLevel { get; set; } = "Information";
}
