using Microsoft.AspNetCore.Builder;

namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// This static class provides extension methods for adding custom middleware to an ASP.NET Core application pipeline.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds middleware to restrict access to the application based on IP addresses.
    /// </summary>
    /// <param name="builder">The IApplicationBuilder instance representing the application pipeline.</param>
    /// <param name="configureOptions">An Action delegate that allows configuring the allowed IP addresses using an IPRestrictionOptions instance.</param>
    /// <returns>The IApplicationBuilder instance to allow method chaining.</returns>
    public static IApplicationBuilder UseIpRestriction(this IApplicationBuilder builder, Action<IPRestrictionOptions> configureOptions)
    {
        var options = new IPRestrictionOptions();
        configureOptions(options);
        return builder.UseMiddleware<IpRestrictionMiddleware>(options);
    }

    /// <summary>
    /// Adds middleware to track user activity by logging request path and timestamp.
    /// </summary>
    /// <param name="builder">The IApplicationBuilder instance representing the application pipeline.</param>
    /// <param name="configureOptions">An Action delegate that allows configuring the user activity tracking behavior using a UserActivityTrackingOptions instance.</param>
    /// <returns>The IApplicationBuilder instance to allow method chaining.</returns>
    public static IApplicationBuilder UseUserActivityTracking(this IApplicationBuilder builder, Action<UserActivityTrackingOptions> configureOptions)
    {
        var options = new UserActivityTrackingOptions();
        configureOptions(options);
        return builder.UseMiddleware<UserActivityTrackingMiddleware>(options);
    }

    /// <summary>
    /// Adds middleware to log information about incoming HTTP requests to the console and a file.
    /// </summary>
    /// <param name="builder">The IApplicationBuilder instance representing the application pipeline.</param>
    /// <returns>The IApplicationBuilder instance to allow method chaining.</returns>
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}
