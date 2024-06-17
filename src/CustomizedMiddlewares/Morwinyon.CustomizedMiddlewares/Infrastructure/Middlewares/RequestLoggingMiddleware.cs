using Microsoft.AspNetCore.Http;

namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// Middleware that logs information about incoming HTTP requests to the console and a file.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the RequestLoggingMiddleware class.
    /// </summary>
    /// <param name="next">The next middleware component in the pipeline.</param>
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware asynchronously for each incoming HTTP request.
    /// Logs the request method, path, query string, and timestamp to the console and a file.
    /// </summary>
    /// <param name="context">The HttpContext object representing the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var log = $"[{DateTime.UtcNow}] {request.Method} {request.Path} {request.QueryString}\n";

        // Log to console
        Console.WriteLine(log);

        // Log to file
        await File.AppendAllTextAsync("request_logs.txt", log);

        await _next(context);
    }
}
