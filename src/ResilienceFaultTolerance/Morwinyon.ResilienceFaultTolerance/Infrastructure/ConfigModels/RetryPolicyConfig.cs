namespace Morwinyon.ResilienceFaultTolerance;
/// <summary>
/// Configuration options for retry policies.
/// </summary>
public class RetryPolicyConfig
{
    /// <summary>
    /// The number of times to retry an operation before considering it failed.
    /// </summary>
    public int RetryCount { get; set; } = 3;  // Set a default value of 3 retries

    /// <summary>
    /// The delay in seconds to wait between retries.
    /// </summary>
    public int RetryDelayInSeconds { get; set; } = 1;  // Set a default value of 1 second delay

    /// <summary>
    /// (Optional) A delegate that specifies which exceptions should be retried.
    /// By default, all exceptions are retried.
    /// </summary>
    public Func<Exception, bool> ShouldRetry { get; set; }

    /// <summary>
    /// (Optional) The maximum total time in seconds to spend retrying operations.
    /// If not set, retries will continue until the configured number of attempts 
    /// is reached.
    /// </summary>
    public int? TotalTimeoutInSeconds { get; set; }
}
