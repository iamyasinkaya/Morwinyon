using Serilog;
using Serilog.Events;

namespace Morwinyon.Logging;

/// <summary>
/// Azure loglama seçenekleri.
/// </summary>
public class AzureOptions
{
    /// <summary>
    /// Azure Application Insights Instrumentation Key.
    /// </summary>
    public string InstrumentationKey { get; set; }

    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog Azure logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }
}
