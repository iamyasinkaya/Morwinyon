using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Morwinyon.Logging;
public class FileLogger : ILogger
{
    private readonly string _categoryName;
    private readonly Func<string, LogLevel, bool> _filter;
    private readonly FileOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileLogger(
        string categoryName,
        Func<string, LogLevel, bool> filter,
        FileOptions options,
        IHttpContextAccessor httpContextAccessor)
    {
        _categoryName = categoryName;
        _filter = filter;
        _options = options;
        _httpContextAccessor = httpContextAccessor;
    }

    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => _filter(_categoryName, logLevel);


    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!_options.IsEnabled || logLevel < (LogLevel)_options.MinimumLogEventLevel) return;

        var logMessage = formatter(state, exception);

        var httpRequest = _httpContextAccessor.HttpContext?.Request;
        var ipAddress = httpRequest?.HttpContext.Connection.RemoteIpAddress.ToString();
        var userAgent = httpRequest?.Headers["User-Agent"].ToString();

        // Log dosyasının yolu
        var logFilePath = _options.FilePath;

        Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

        if (File.Exists(logFilePath))
        {
            // Log dosyasının maksimum yaşını kontrol etme
            if (_options.MaximumFileAge.HasValue)
            {
                var logFileInfo = new FileInfo(logFilePath);
                var fileAge = DateTime.Now - logFileInfo.CreationTime;
                if (fileAge > _options.MaximumFileAge.Value)
                {
                    // Dosya yaş limitine ulaşıldığında dosyayı sil
                    File.Delete(logFilePath);
                }
            }

            // Log dosyasının maksimum boyutunu kontrol etme
            if (_options.MaximumFileSizeBytes.HasValue)
            {
                var logFileInfo = new FileInfo(logFilePath);
                if (logFileInfo.Length > _options.MaximumFileSizeBytes)
                {
                    // Dosya boyutu limitine ulaşıldığında dosyayı sil
                    File.Delete(logFilePath);
                }
            }

            // Log dosyasının maksimum sayısını kontrol etme
            if (_options.MaximumNumberOfFiles.HasValue)
            {
                var directoryPath = Path.GetDirectoryName(logFilePath);
                var logFiles = Directory.GetFiles(directoryPath, "*.log");
                if (logFiles.Length >= _options.MaximumNumberOfFiles)
                {
                    // Minimum sayıya ulaşıldığında eski dosyaları sil
                    var filesToDelete = logFiles.OrderBy(f => new FileInfo(f).CreationTime).Take(logFiles.Length - _options.MaximumNumberOfFiles.Value + 1);
                    foreach (var fileToDelete in filesToDelete)
                    {
                        File.Delete(fileToDelete);
                    }
                }
            }
        }

    

        // Log dosyasına yazma işlemi
        using (var streamWriter = File.AppendText(logFilePath))
        {
            streamWriter.WriteLine($"{DateTime.Now} [{logLevel}] [{ipAddress}] [{userAgent}] - {logMessage}");
        }
    }

}