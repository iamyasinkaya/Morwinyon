using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace Morwinyon.Caching
{

    /// <summary>
    /// Implementation of the ICacheService<T> interface using Redis as the caching backend.
    /// Manages caching of generic type T in a Redis database.
    /// </summary>
    /// <typeparam name="T">Type of the objects to be cached.</typeparam>
    public class RedisCacheService<T> : ICacheService<T>
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _redisDatabase;
        private readonly object _redisLock = new object();

        /// <summary>
        /// Initializes a new instance of the RedisCacheService<T> class.
        /// </summary>
        /// <param name="redisConnection">The IConnectionMultiplexer instance representing the connection to Redis.</param>
        public RedisCacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            _redisDatabase = _redisConnection.GetDatabase();
        }

        /// <inheritdoc/>
        public T Get(string key)
        {
            lock (_redisLock)
            {
                if (TryGetValue(key, out T value))
                {
                    return value;
                }
            }

            throw new KeyNotFoundException($"Key not found in the cache: {key}");
        }

        /// <inheritdoc/>
        public void Set(string key, T value, TimeSpan expirationTime)
        {
            try
            {
                _redisDatabase.StringSet(key, SerializationExtensions.Serialize(value), expirationTime);
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public bool TryGetValue(string key, out T value)
        {
            try
            {
                var redisValue = _redisDatabase.StringGet(key);
                if (redisValue.HasValue)
                {
                    value = SerializationExtensions.Deserialize<T>(redisValue);
                    return true;
                }
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
            }

            value = default;
            return false;
        }

        /// <inheritdoc/>
        public bool ContainsKey(string key)
        {
            try
            {
                return _redisDatabase.KeyExists(key);
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            try
            {
                _redisDatabase.KeyDelete(key);
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            try
            {
                var endpoints = _redisConnection.GetEndPoints();
                foreach (var endpoint in endpoints)
                {
                    var server = _redisConnection.GetServer(endpoint);
                    server.FlushDatabase();
                }
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetAllKeys()
        {
            try
            {
                var endpoints = _redisConnection.GetEndPoints();
                var keys = new List<string>();

                foreach (var endpoint in endpoints)
                {
                    var server = _redisConnection.GetServer(endpoint);

                    foreach (var key in server.Keys())
                    {
                        keys.Add(key.ToString());
                    }
                }

                return keys;
            }
            catch (RedisConnectionException ex)
            {
                // Handle Redis connection exceptions appropriately
                Console.WriteLine($"Redis connection error: {ex.Message}");
                return Array.Empty<string>();
            }
        }

        /// <inheritdoc/>
        public IDictionary<string, T> GetBatch(IEnumerable<string> keys)
        {
            var result = new Dictionary<string, T>();
            foreach (var key in keys)
            {
                if (TryGetValue(key, out T value))
                {
                    result[key] = value;
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public T GetOrSet(string key, T value, TimeSpan expirationTime)
        {
            if (TryGetValue(key, out T existingValue))
            {
                return existingValue;
            }

            Set(key, value, expirationTime);
            return value;
        }
    }
}