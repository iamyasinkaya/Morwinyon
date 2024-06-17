namespace Morwinyon.RateLimiting;

/// <summary>
/// Represents the result of a rate limit check.
/// </summary>
public class RateLimitResult
{
    /// <summary>
    /// Gets or sets the status of the rate limit check.
    /// </summary>
    public RateLimitStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the message associated with the rate limit check.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the remaining number of requests allowed in the current time window.
    /// </summary>
    public int RemainingRequests { get; set; }

    /// <summary>
    /// Gets or sets the time after which the client can retry making requests.
    /// </summary>
    public TimeSpan RetryAfter { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the client making the request.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the total number of requests made by the client in the current time window.
    /// </summary>
    public int RequestCount { get; set; }

    /// <summary>
    /// Gets or sets the time when the rate limit window will reset.
    /// </summary>
    public DateTime ResetTime { get; set; }
}