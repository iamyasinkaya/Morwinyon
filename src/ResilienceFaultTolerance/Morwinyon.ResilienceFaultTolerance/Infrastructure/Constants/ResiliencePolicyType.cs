namespace Morwinyon.ResilienceFaultTolerance;

/// <summary>
/// Defines the types of resilience policies available.
/// </summary>
public enum ResiliencePolicyType
{
    /// <summary>
    /// Indicates a retry policy with configurable retry attempts and delays.
    /// </summary>
    Retry,
    /// <summary>
    /// Indicates a circuit breaker policy that trips after a certain number of failures 
    /// and prevents further calls for a specified duration.
    /// </summary>
    CircuitBreaker
}
