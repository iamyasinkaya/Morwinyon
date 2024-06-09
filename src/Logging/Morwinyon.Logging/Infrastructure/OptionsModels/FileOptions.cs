using Serilog.Events;

namespace Morwinyon.Logging;

/// <summary>
/// Log dosyaları için yapılandırma seçeneklerini temsil eder.
/// </summary>
public class FileOptions
{
    /// <summary>
    /// Minimum log olay seviyesini alır veya ayarlar.
    /// </summary>
    public LogEventLevel MinimumLogEventLevel { get; set; }

    /// <summary>
    /// Log dosyasının yolunu alır veya ayarlar.
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Log dosyasının maksimum boyutunu alır veya ayarlar.
    /// </summary>
    public long? MaximumFileSizeBytes { get; set; }

    /// <summary>
    /// Log dosyasının maksimum yaşını alır veya ayarlar.
    /// </summary>
    public TimeSpan? MaximumFileAge { get; set; }

    /// <summary>
    /// Log dosyasının maksimum sayısını alır veya ayarlar.
    /// </summary>
    public int? MaximumNumberOfFiles { get; set; }

    /// <summary>
    /// Log mesajı formatını alır veya ayarlar.
    /// </summary>
    public string LogMessageTemplate { get; set; }

    /// <summary>
    /// Dosyanın rotasyon stratejisini alır veya ayarlar.
    /// </summary>
    public FileRotationStrategy RotationStrategy { get; set; }

    /// <summary>
    /// Dosya boyutu limitine ulaşıldığında nasıl davranılacağını alır veya ayarlar.
    /// </summary>
    public FileSizeLimitBehavior SizeLimitBehavior { get; set; }

    /// <summary>
    /// Log dosyasının başlangıçta oluşturulup oluşturulmayacağını belirtir.
    /// </summary>
    public bool CreateAtStartup { get; set; }
    public bool IsEnabled { get; set; }


}