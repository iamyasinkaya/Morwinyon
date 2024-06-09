using Serilog;
using Serilog.Events;

namespace Morwinyon.Logging;

/// <summary>
/// Serilog konsol loglama seçenekleri.
/// </summary>
public class ConsoleOptions
{
    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Log formatı. Varsayılan değer: {Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}
    /// </summary>
    public string OutputTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    /// <summary>
    /// Renkli çıktı kullanılsın mı? Varsayılan değer: true
    /// </summary>
    public bool UseColors { get; set; } = true;

    /// <summary>
    /// Eğer renkli çıktı kullanılıyorsa, log seviyeleri için renkler.
    /// </summary>
    public ConsoleColor DebugColor { get; set; } = ConsoleColor.Gray;
    public ConsoleColor InformationColor { get; set; } = ConsoleColor.White;
    public ConsoleColor WarningColor { get; set; } = ConsoleColor.Yellow;
    public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
    public ConsoleColor FatalColor { get; set; } = ConsoleColor.DarkRed;

    /// <summary>
    /// Konsol çıktısı otomatik olarak temizlensin mi? Varsayılan değer: false
    /// </summary>
    public bool AutoFlush { get; set; } = false;

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog konsol logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }

    public bool IsEnabled { get; set; } = true;
}
