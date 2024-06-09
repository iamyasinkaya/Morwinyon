using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Morwinyon.Logging;

public class FileLoggerProvider : ILoggerProvider
{
    private readonly Func<string, LogLevel, bool> _filter;
    private readonly FileOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileLoggerProvider(Func<string, LogLevel, bool> filter, FileOptions options, IHttpContextAccessor httpContextAccessor)
    {
        _filter = filter;
        _options = options;
        _httpContextAccessor = httpContextAccessor;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(categoryName, _filter, _options, _httpContextAccessor);
    }

    public void Dispose() { }
}