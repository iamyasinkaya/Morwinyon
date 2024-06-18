namespace Morwinyon.ResilienceFaultTolerance;

/// <summary>
/// Configuration options for circuit breaker policies.
/// </summary>
public class CircuitBreakerPolicyConfig
{
    /// <summary>
    /// The number of exceptions allowed to occur before tripping the circuit breaker 
    /// and transitioning to the open state.
    /// </summary>
    public int ExceptionsAllowedBeforeBreaking { get; set; } = 5;  // Set a default value of 5 allowed exceptions

    /// <summary>
    /// The duration in seconds to stay in the open state (circuit breaker tripped) 
    /// before attempting to reset and transition back to the closed state.
    /// </summary>
    public int DurationOfBreakInSeconds { get; set; } = 10;  // Set a default value of 10 seconds break
}
