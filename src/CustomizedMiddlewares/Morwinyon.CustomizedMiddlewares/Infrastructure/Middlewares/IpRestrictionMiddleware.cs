using Microsoft.AspNetCore.Http;

namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// Middleware that restricts access to a web application based on IP addresses.
/// </summary>
public class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IPRestrictionOptions _options;

    /// <summary>
    /// Initializes a new instance of the IpRestrictionMiddleware class.
    /// </summary>
    /// <param name="next">The next middleware component in the pipeline.</param>
    /// <param name="options">An instance of the IPRestrictionOptions class containing the allowed IP addresses.</param>
    public IpRestrictionMiddleware(RequestDelegate next, IPRestrictionOptions options)
    {
        _next = next;
        _options = options;
    }

    /// <summary>
    /// Invokes the middleware asynchronously for each incoming HTTP request.
    /// </summary>
    /// <param name="context">The HttpContext object representing the current request.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress?.ToString();
        if (!_options.AllowedIPs.Contains(remoteIp))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden IP");
            return;
        }

        await _next(context);
    }
}
