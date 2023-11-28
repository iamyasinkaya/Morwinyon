namespace Morwinyon.Caching { 

/// <summary>
/// Represents the configuration options for caching, including Redis and memory cache settings.
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// Gets or sets the configuration for Redis cache.
    /// </summary>
    public RedisCacheConfig RedisConfig { get; set; }

    /// <summary>
    /// Gets or sets the configuration for in-memory cache.
    /// </summary>
    public MemoryCacheConfig MemoryConfig { get; set; }
}
}