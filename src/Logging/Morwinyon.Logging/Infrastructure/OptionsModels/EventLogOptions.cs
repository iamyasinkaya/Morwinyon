using Serilog;
using Serilog.Events;
using System.Diagnostics;

namespace Morwinyon.Logging;
/// <summary>
/// Event Log yapılandırma seçenekleri.
/// </summary>
public class EventLogOptions
{
    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Event Log kaydının kaynak adı. Varsayılan değer: "Application"
    /// </summary>
    public string LogName { get; set; } = "Application";

    /// <summary>
    /// Event Log kaydının olay kaynağı adı. Varsayılan değer: "MyApplication"
    /// </summary>
    public string SourceName { get; set; } = "MyApplication";

    /// <summary>
    /// Event Log kaydının günlüğe yazıldığı bilgisayar adı. Varsayılan değer: "."
    /// </summary>
    public string MachineName { get; set; } = ".";

    /// <summary>
    /// Event Log kaydı işlenirken uygulanan event kayıt opsiyonu. Varsayılan değer: EventLogEntryType.Information
    /// </summary>
    public EventLogEntryType EntryType { get; set; } = EventLogEntryType.Information;

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog Event Log logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }
}