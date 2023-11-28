using System;
using System.Collections.Generic;

namespace Morwinyon.Caching { 

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICacheService<T>
{
    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    T Get(string key);

    /// <summary>
    /// Adds the value associated with the specified key to the cache.
    /// </summary>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Value to be added to the cache.</param>
    /// <param name="expirationTime">Time duration for the value to remain in the cache.</param>
    void Set(string key, T value, TimeSpan expirationTime);

    /// <summary>
    /// Tries to get the value associated with the specified key and indicates whether the operation was successful.
    /// </summary>
    /// <param name="key">Cache key.</param>
    /// <param name="value">The value associated with the specified key (if it exists).</param>
    /// <returns>True if the value is successfully retrieved, otherwise false.</returns>
    bool TryGetValue(string key, out T value);

    /// <summary>
    /// Checks if the specified key is present in the cache.
    /// </summary>
    /// <param name="key">Cache key.</param>
    /// <returns>True if the key is present in the cache, otherwise false.</returns>
    bool ContainsKey(string key);

    /// <summary>
    /// Removes the value associated with the specified key from the cache.
    /// </summary>
    /// <param name="key">Cache key.</param>
    void Remove(string key);

    /// <summary>
    /// Clears the cache, removing all items.
    /// </summary>
    void Clear();

    /// <summary>
    /// Retrieves all keys present in the cache.
    /// </summary>
    /// <returns>All keys present in the cache.</returns>
    IEnumerable<string> GetAllKeys();

    /// <summary>
    /// Retrieves items associated with the specified cache keys in bulk.
    /// </summary>
    /// <param name="keys">Cache keys to retrieve.</param>
    /// <returns>Cache items associated with the specified keys.</returns>
    IDictionary<string, T> GetBatch(IEnumerable<string> keys);

    /// <summary>
    /// Adds the value associated with the specified key to the cache, but does not add if the key already exists.
    /// </summary>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Value to be added to the cache.</param>
    /// <param name="expirationTime">Time duration for the value to remain in the cache.</param>
    /// <returns>The value representing the added item (if added), otherwise the default value.</returns>
    T GetOrSet(string key, T value, TimeSpan expirationTime);
}
}