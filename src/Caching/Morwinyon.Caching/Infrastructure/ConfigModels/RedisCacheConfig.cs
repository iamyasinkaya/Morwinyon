namespace Morwinyon.Caching
{

    /// <summary>
    /// Represents the configuration options for a Redis cache.
    /// </summary>
    public class RedisCacheConfig
    {
        /// <summary>
        /// Gets or sets the Redis server endpoints in the format "host:port".
        /// </summary>
        public string Endpoints { get; set; } = "localhost:6379";

        /// <summary>
        /// Gets or sets the connection timeout for the Redis cache in milliseconds.
        /// </summary>
        public int ConnectTimeout { get; set; } = 5000;

        /// <summary>
        /// Gets or sets the synchronization timeout for the Redis cache in milliseconds.
        /// </summary>
        public int SyncTimeout { get; set; } = 1000;

        /// <summary>
        /// Gets or sets a value indicating whether to abort on connection failure in the Redis cache.
        /// </summary>
        public bool AbortOnConnectFail { get; set; } = false;

        /// <summary>
        /// Gets or sets the password for the Redis cache (if applicable).
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Redis database index to be used for caching.
        /// </summary>
        public int DatabaseIndex { get; set; } = 0;

        /// <summary>
        /// Gets or sets the prefix to be applied to all cache keys in the Redis cache.
        /// </summary>
        public string KeyPrefix { get; set; }

        /// <summary>
        /// Gets or sets whether SSL/TLS should be used for the Redis connection.
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// Gets or sets the client name to be used for the Redis connection.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of connections allowed to the Redis server.
        /// </summary>
        public int MaxConnections { get; set; } = 10;

        /// <summary>
        /// Gets or sets the minimum number of connections maintained to the Redis server.
        /// </summary>
        public int MinConnections { get; set; } = 1;


        /// <summary>
        /// Gets or sets the maximum number of operations that can be pipelined (queued) before waiting for a response from the Redis server.
        /// </summary>
        public int MaxPipelinedOperations { get; set; } = 100;

        /// <summary>
        /// Gets or sets whether to allow admin operations on the Redis server.
        /// </summary>
        public bool AllowAdmin { get; set; } = false;
    }
}