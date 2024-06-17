using Microsoft.AspNetCore.Http;

namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// Middleware that tracks user activity by logging request path and timestamp based on UserActivityTrackingOptions.
/// </summary>
public class UserActivityTrackingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly UserActivityTrackingOptions _options;

    /// <summary>
    /// Initializes a new instance of the UserActivityTrackingMiddleware class.
    /// </summary>
    /// <param name="next">The next middleware component in the pipeline.</param>
    /// <param name="options">An instance of the UserActivityTrackingOptions class 
    /// specifying logging behavior.</param>
    public UserActivityTrackingMiddleware(RequestDelegate next, UserActivityTrackingOptions options)
    {
        _next = next;
        _options = options;
    }

    /// <summary>
    /// Invokes the middleware asynchronously for each incoming HTTP request.
    /// Logs request path and timestamp to the console if enabled in options, 
    /// and to a file specified in options if the path is not empty.
    /// </summary>
    /// <param name="context">The HttpContext object representing the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var requestPath = context.Request.Path;
        var timestamp = DateTime.UtcNow;

        if (_options.LogToConsole)
        {
            Console.WriteLine($"Request Path: {requestPath}, Timestamp: {timestamp}");
        }

        if (!string.IsNullOrEmpty(_options.LogFilePath))
        {
            await File.AppendAllTextAsync(_options.LogFilePath, $"Request Path: {requestPath}, Timestamp: {timestamp}\n");
        }

        await _next(context);
    }
}
