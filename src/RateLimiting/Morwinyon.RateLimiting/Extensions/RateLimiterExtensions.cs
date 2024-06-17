using Microsoft.Extensions.DependencyInjection;

namespace Morwinyon.RateLimiting;

/// <summary>
/// Provides extension methods for adding rate limiting functionality to an IServiceCollection.
/// </summary>
public static class RateLimiterExtensions
{
    /// <summary>
    /// Adds a RateLimiter instance to the IServiceCollection with the specified configuration.
    /// </summary>
    /// <typeparam name="T">The type of key used to identify clients.</typeparam>
    /// <param name="services">The IServiceCollection to add the RateLimiter to.</param>
    /// <param name="maxRequests">The maximum number of allowed requests within the time period.</param>
    /// <param name="timePeriod">The time window for rate limiting.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    public static IServiceCollection AddRateLimiter<T>(this IServiceCollection services, int maxRequests, TimeSpan timePeriod)
    {
        var rateLimiter = new RateLimiter<T>(maxRequests, timePeriod);
        services.AddSingleton(rateLimiter);
        return services;
    }
}
