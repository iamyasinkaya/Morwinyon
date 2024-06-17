using System.Collections.Concurrent;

namespace Morwinyon.RateLimiting;

/// <summary>
/// A generic class that implements rate limiting for clients identified by a key of type T.
/// It enforces a maximum number of requests (`maxRequests`) within a specified time period (`timePeriod`).
/// </summary>
public class RateLimiter<T>
{
    private readonly int _maxRequests;
    private readonly TimeSpan _timePeriod;
    private readonly ConcurrentDictionary<T, RequestCounter> _clients = new ConcurrentDictionary<T, RequestCounter>();

    /// <summary>
    /// Initializes the RateLimiter instance with the specified `maxRequests` and `timePeriod`.
    /// </summary>
    /// <param name="maxRequests">The maximum number of allowed requests within the time period.</param>
    /// <param name="timePeriod">The time window for rate limiting.</param>
    public RateLimiter(int maxRequests, TimeSpan timePeriod)
    {
        _maxRequests = maxRequests;
        _timePeriod = timePeriod;
    }

    /// <summary>
    /// Checks if a request from a client identified by `clientId` is allowed.
    /// </summary>
    /// <param name="clientId">The key identifying the client.</param>
    /// <returns>A RateLimitResult object indicating the request status, remaining requests, retry information, etc.</returns>
    public RateLimitResult IsRequestAllowed(T clientId)
    {
        var requestCounter = _clients.GetOrAdd(clientId, _ => new RequestCounter(_timePeriod));
        return requestCounter.IsRequestAllowed(clientId.ToString(), _maxRequests);
    }

    private class RequestCounter
    {
        private readonly TimeSpan _timePeriod;
        private int _requestCount;
        private DateTime _windowStart;

        /// <summary>
        /// Initializes the RequestCounter with the specified `timePeriod`.
        /// </summary>
        /// <param name="timePeriod">The time window for rate limiting.</param>
        public RequestCounter(TimeSpan timePeriod)
        {
            _timePeriod = timePeriod;
            _windowStart = DateTime.UtcNow;
        }

        /// <summary>
        /// Determines if a request from the client is allowed based on the current rate limit.
        /// </summary>
        /// <param name="clientId">The string representation of the client key.</param>
        /// <param name="maxRequests">The maximum number of allowed requests within the time period.</param>
        /// <returns>A RateLimitResult object indicating the request status, remaining requests, retry information, etc.</returns>
        public RateLimitResult IsRequestAllowed(string clientId, int maxRequests)
        {
            var now = DateTime.UtcNow;
            if (now - _windowStart > _timePeriod)
            {
                _windowStart = now;
                _requestCount = 0;
            }

            if (_requestCount >= maxRequests)
            {
                return new RateLimitResult
                {
                    Status = RateLimitStatus.Denied,
                    Message = "Too many requests. Please try again later.",
                    RemainingRequests = 0,
                    RetryAfter = _timePeriod - (now - _windowStart),
                    ClientId = clientId,
                    RequestCount = _requestCount,
                    ResetTime = _windowStart.Add(_timePeriod)
                };
            }

            _requestCount++;
            return new RateLimitResult
            {
                Status = RateLimitStatus.Allowed,
                Message = "Request successful.",
                RemainingRequests = maxRequests - _requestCount,
                RetryAfter = TimeSpan.Zero,
                ClientId = clientId,
                RequestCount = _requestCount,
                ResetTime = _windowStart.Add(_timePeriod)
            };
        }
    }
}