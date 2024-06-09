using Serilog.Events;
using Serilog;

namespace Morwinyon.Logging;

/// <summary>
/// Serilog Elasticsearch loglama seçenekleri.
/// </summary>
public class ElasticsearchOptions
{
    /// <summary>
    /// Log seviyesi. Varsayılan değer: LogEventLevel.Information
    /// </summary>
    public LogEventLevel MinimumLevel { get; set; } = LogEventLevel.Information;

    /// <summary>
    /// Elasticsearch sunucu adresi.
    /// </summary>
    public Uri ServerUrl { get; set; }

    /// <summary>
    /// Elasticsearch index adı. Varsayılan değer: "logs-{0:yyyy.MM.dd}"
    /// </summary>
    public string IndexName { get; set; } = "logs-{0:yyyy.MM.dd}";

    /// <summary>
    /// Elasticsearch kullanıcı adı (opsiyonel).
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Elasticsearch parola (opsiyonel).
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Elasticsearch bağlantı zaman aşım süresi (opsiyonel). Varsayılan değer: 30 saniye.
    /// </summary>
    public TimeSpan ConnectionTimeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Logların birikmesi için maksimum bekleme süresi (opsiyonel). Varsayılan değer: 1 saniye.
    /// </summary>
    public TimeSpan BatchPostingLimit { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// Logların birikmesi için maksimum log sayısı (opsiyonel). Varsayılan değer: 1000.
    /// </summary>
    public int BatchSizeLimit { get; set; } = 1000;

    /// <summary>
    /// Ek bir ayar yapmak isteyenler için Serilog Elasticsearch logger konfigürasyonu.
    /// </summary>
    public Action<LoggerConfiguration> LoggerConfiguration { get; set; }

    public bool IsEnabled { get; set; }
}