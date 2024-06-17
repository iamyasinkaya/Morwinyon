namespace Morwinyon.RateLimiting;

public enum RateLimitStatus
{
    /// <summary>
    /// Indicates that the request is allowed.
    /// </summary>
    Allowed,

    /// <summary>
    /// Indicates that the request is denied due to exceeding the rate limit.
    /// </summary>
    Denied
}
