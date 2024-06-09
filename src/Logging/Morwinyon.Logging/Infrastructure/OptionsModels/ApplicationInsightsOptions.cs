namespace Morwinyon.Logging;

/// <summary>
/// Application Insights yapılandırma seçenekleri.
/// </summary>
public class ApplicationInsightsOptions
{
    /// <summary>
    /// Application Insights özel anahtar.
    /// </summary>
    public string InstrumentationKey { get; set; }

    /// <summary>
    /// Geliştirme ortamında çalışırken gönderilecek verilerin engellenip engellenmeyeceği.
    /// </summary>
    public bool EnableDebugLogger { get; set; } = true;

    /// <summary>
    /// Application Insights'e gönderilen logların otomatik olarak gönderilip gönderilmeyeceği.
    /// </summary>
    public bool AutoFlush { get; set; } = true;

    /// <summary>
    /// Application Insights'in etkinleştirilip etkinleştirilmeyeceği.
    /// </summary>
    public bool Enabled { get; set; } = true;
}
