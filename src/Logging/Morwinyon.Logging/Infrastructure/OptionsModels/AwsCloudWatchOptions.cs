using Serilog.Events;
using Serilog;

namespace Morwinyon.Logging;

/// <summary>
/// AWS CloudWatch loglama seçenekleri.
/// </summary>
public class AwsCloudWatchOptions
{
    /// <summary>
    /// AWS bölge adı.
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// AWS kimlik bilgileri.
    /// </summary>
    public AwsCredentials Credentials { get; set; }

    /// <summary>
    /// Log grubu adı.
    /// </summary>
    public string LogGroupName { get; set; }

    /// <summary>
    /// Log akışı adı.
    /// </summary>
    public string LogStreamName { get; set; }

    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog AWS CloudWatch logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }
}

/// <summary>
/// AWS kimlik bilgileri.
/// </summary>
public class AwsCredentials
{
    /// <summary>
    /// Erişim anahtarı.
    /// </summary>
    public string AccessKey { get; set; }

    /// <summary>
    /// Gizli erişim anahtarı.
    /// </summary>
    public string SecretKey { get; set; }
}