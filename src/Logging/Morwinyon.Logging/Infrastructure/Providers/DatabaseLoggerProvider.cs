using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Morwinyon.Logging;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly Func<string, LogLevel, bool> _filter;
    private readonly DatabaseOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DatabaseLoggerProvider(Func<string, LogLevel, bool> filter, DatabaseOptions options, IHttpContextAccessor httpContextAccessor)
    {
        _filter = filter;
        _options = options;
        _httpContextAccessor = httpContextAccessor;
    }
    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(categoryName, _options);
    }

    public void Dispose() { }
}
