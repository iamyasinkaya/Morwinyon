namespace Morwinyon.Caching
{

    /// <summary>
    /// Enumerates the types of caching mechanisms supported.
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// Represents an in-memory cache.
        /// </summary>
        Memory = 0,

        /// <summary>
        /// Represents a Redis cache.
        /// </summary>
        Redis = 1
    }
}