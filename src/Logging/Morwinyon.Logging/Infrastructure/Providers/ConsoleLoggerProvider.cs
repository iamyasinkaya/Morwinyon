using Microsoft.Extensions.Logging;

namespace Morwinyon.Logging;


public class ConsoleLoggerProvider : ILoggerProvider
{
    private readonly ConsoleOptions _options;

    public ConsoleLoggerProvider(ConsoleOptions options)
    {
        _options = options;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger(categoryName, _options);
    }

    public void Dispose()
    {
    }
}