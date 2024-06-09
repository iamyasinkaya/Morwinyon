namespace Morwinyon.Logging;

/// <summary>
/// OpenTelemetry yapılandırma seçenekleri.
/// </summary>
public class OpenTelemetryOptions
{
    /// <summary>
    /// OpenTelemetry'in etkinleştirilip etkinleştirilmeyeceği. Varsayılan değer: false
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// OpenTelemetry'nin bağlanacağı adres.
    /// </summary>
    public Uri Endpoint { get; set; }

    /// <summary>
    /// OpenTelemetry için kaynak adı. Varsayılan değer: null
    /// </summary>
    public string ResourceName { get; set; }

    /// <summary>
    /// OpenTelemetry için hedef adı. Varsayılan değer: "my-service"
    /// </summary>
    public string ServiceName { get; set; } = "my-service";

    /// <summary>
    /// OpenTelemetry için veri alıcıları.
    /// </summary>
    public OpenTelemetryDataCollectors DataCollectors { get; set; } = new OpenTelemetryDataCollectors();
}

/// <summary>
/// OpenTelemetry veri alıcıları.
/// </summary>
public class OpenTelemetryDataCollectors
{
    /// <summary>
    /// HTTP istekleri için veri toplama ayarı.
    /// </summary>
    public bool HttpEnabled { get; set; } = true;

    /// <summary>
    /// Veritabanı sorguları için veri toplama ayarı.
    /// </summary>
    public bool DatabaseEnabled { get; set; } = true;

    /// <summary>
    /// Kullanıcı tanımlı operasyonlar için veri toplama ayarı.
    /// </summary>
    public bool CustomOperationsEnabled { get; set; } = true;
}