using Serilog.Events;
using Serilog;

namespace Morwinyon.Logging;

/// <summary>
/// Serilog veritabanı loglama seçenekleri.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Veritabanı bağlantı dizesi.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Log tablosunun adı. Varsayılan değer: Logs
    /// </summary>
    public string TableName { get; set; } = "Logs";

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog veritabanı logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }

    /// <summary>
    /// Log tablosundaki sütunların yapılandırılması.
    /// </summary>
    public DatabaseColumnsConfiguration Columns { get; set; } = new DatabaseColumnsConfiguration();
    public DatabaseType DatabaseType { get; set; }

    public bool IsEnabled { get; set; }
}

/// <summary>
/// Veritabanı log tablosundaki sütunların yapılandırması.
/// </summary>
public class DatabaseColumnsConfiguration
{
    /// <summary>
    /// Zaman damgası sütununun adı. Varsayılan değer: Timestamp
    /// </summary>
    public string TimestampColumnName { get; set; } = "Timestamp";

    /// <summary>
    /// Seviye sütununun adı. Varsayılan değer: Level
    /// </summary>
    public string LevelColumnName { get; set; } = "Level";

    /// <summary>
    /// Mesaj sütununun adı. Varsayılan değer: Message
    /// </summary>
    public string MessageColumnName { get; set; } = "Message";

    /// <summary>
    /// Özel bir ek sütununun adı. Varsayılan değer: null
    /// </summary>
    public string CustomColumnName { get; set; }
}