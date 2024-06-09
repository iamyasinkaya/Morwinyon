using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace Morwinyon.Logging;

public class ConsoleLogger : ILogger
{
    private readonly string _categoryName;
    private readonly ConsoleOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConsoleLogger(string categoryName, ConsoleOptions options = null, IHttpContextAccessor httpContextAccessor = null)
    {
        _categoryName = categoryName;
        _options = options ?? new ConsoleOptions();
        _httpContextAccessor = httpContextAccessor;
    }

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => _options.IsEnabled && ConvertLogLevelToSerilogLevel(logLevel) >= _options.MinimumLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        string message = formatter(state, exception);

        // Request bilgilerini alalım
        string requestInfo = GetRequestInfo();

        // Örnek olarak, konsola loglama yapalım
        Console.ForegroundColor = GetConsoleColor(logLevel);
        Console.WriteLine($"[{DateTime.Now}] [{_categoryName}] [{logLevel}] {requestInfo} - {message}");
        Console.ResetColor();

        // Burada dosyaya, veritabanına veya başka bir kaynağa loglama yapabilirsiniz
    }

    private string GetRequestInfo()
    {
        // HttpContextAccessor kullanarak request bilgilerini alalım
        var httpRequest = _httpContextAccessor?.HttpContext?.Request;
        if (httpRequest == null)
            return string.Empty;

        var ipAddress = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
        var userAgent = httpRequest.Headers["User-Agent"].ToString();

        return $"[IP: {ipAddress}, User-Agent: {userAgent}]";
    }

    private ConsoleColor GetConsoleColor(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Debug => _options.DebugColor,
            LogLevel.Information => _options.InformationColor,
            LogLevel.Warning => _options.WarningColor,
            LogLevel.Error => _options.ErrorColor,
            LogLevel.Critical => _options.FatalColor,
            _ => ConsoleColor.Gray,
        };
    }

    // LogLevel'ü Serilog.Events.LogEventLevel türüne dönüştüren yöntem
    private LogEventLevel ConvertLogLevelToSerilogLevel(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Critical => LogEventLevel.Fatal,
            _ => LogEventLevel.Information,
        };
    }
}