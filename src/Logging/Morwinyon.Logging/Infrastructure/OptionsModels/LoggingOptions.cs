namespace Morwinyon.Logging;

public class LoggingOptions
{
    public ApplicationInsightsOptions ApplicationInsightsOptions { get; set; }
    public AwsCloudWatchOptions AwsCloudWatchOptions { get; set; }
    public AzureOptions AzureOptions { get; set; }
    public ConsoleOptions ConsoleOptions { get; set; }
    public DatabaseOptions DatabaseOptions { get; set; }
    public ElasticsearchOptions ElasticsearchOptions { get; set; }
    public EventLogOptions EventLogOptions { get; set; }
    public FileOptions FileOptions { get; set; }
    public OpenTelemetryOptions OpenTelemetryOptions { get; set; }
}
