namespace Morwinyon.RateLimiting;

/// <summary>
/// Provides helper methods for rate limiting.
/// </summary>
public static class RateLimitHelper
{
    /// <summary>
    /// Generates a client identifier by combining the client's IP address and the endpoint they are accessing.
    /// </summary>
    /// <param name="ipAddress">The client's IP address.</param>
    /// <param name="endpoint">The endpoint being accessed by the client.</param>
    /// <returns>A string representing the combined client identifier.</returns>
    public static string GenerateClientId(string ipAddress, string endpoint)
    {
        return $"{ipAddress}:{endpoint}";
    }
}
