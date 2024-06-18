using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Morwinyon.ResilienceFaultTolerance;

/// <summary>
/// Provides helper methods for creating common resilience policies.
/// </summary>
public static class ResiliencePolicyHelper
{
    /// <summary>
    /// Creates a retry policy based on the provided configuration.
    /// </summary>
    /// <param name="config">The configuration object for the retry policy.</param>
    /// <returns>An IAsyncPolicy object representing the created retry policy.</returns>
    public static IAsyncPolicy CreateRetryPolicy(RetryPolicyConfig config)
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(config.RetryCount,
                retryAttempt => TimeSpan.FromSeconds(config.RetryDelayInSeconds));
    }

    /// <summary>
    /// Creates a circuit breaker policy based on the provided configuration.
    /// </summary>
    /// <param name="config">The configuration object for the circuit breaker policy.</param>
    /// <returns>An IAsyncPolicy object representing the created circuit breaker policy.</returns>
    public static IAsyncPolicy CreateCircuitBreakerPolicy(CircuitBreakerPolicyConfig config)
    {
        return Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: config.ExceptionsAllowedBeforeBreaking,
                durationOfBreak: TimeSpan.FromSeconds(config.DurationOfBreakInSeconds));
    }
}
