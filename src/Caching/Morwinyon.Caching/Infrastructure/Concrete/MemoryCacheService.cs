using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Morwinyon.Caching { 

/// <summary>
/// Implementation of the ICacheService<T> interface using the IMemoryCache.
/// Manages caching of generic type T in memory.
/// </summary>
/// <typeparam name="T">Type of the objects to be cached.</typeparam>
public class MemoryCacheService<T> : ICacheService<T>
{
    private readonly IMemoryCache _memoryCache;
    private readonly List<string> _cacheKeys;
    private readonly object _cacheKeysLock = new object();

    /// <summary>
    /// Initializes a new instance of the MemoryCacheService<T> class.
    /// </summary>
    /// <param name="memoryCache">The IMemoryCache instance to be used for caching.</param>
    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _cacheKeys = new List<string>();
    }

    /// <inheritdoc/>
    public T Get(string key)
    {
        if (TryGetValue(key, out T value))
        {
            return value;
        }

        throw new KeyNotFoundException($"Key not found in the cache: {key}. Cache keys: {string.Join(", ", _cacheKeys)}");
    }

    /// <inheritdoc/>
    public void Set(string key, T value, TimeSpan expirationTime)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime,
            SlidingExpiration = TimeSpan.MaxValue
        };

        _memoryCache.Set(key, value, cacheEntryOptions);
        TrackKey(key);
    }

    /// <inheritdoc/>
    public bool TryGetValue(string key, out T value)
    {
        return _memoryCache.TryGetValue(key, out value);
    }

    /// <inheritdoc/>
    public bool ContainsKey(string key)
    {
        return _memoryCache.TryGetValue(key, out _);
    }

    /// <inheritdoc/>
    public void Remove(string key)
    {
        _memoryCache.Remove(key);
        UntrackKey(key);
    }

    /// <inheritdoc/>
    public void Clear()
    {
        lock (_cacheKeysLock)
        {
            foreach (var key in _cacheKeys)
            {
                _memoryCache.Remove(key);
            }
            _cacheKeys.Clear();
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetAllKeys()
    {
        lock (_cacheKeysLock)
        {
            return _cacheKeys.ToList();
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

    private void TrackKey(string key)
    {
        lock (_cacheKeysLock)
        {
            _cacheKeys.Add(key);
        }
    }

    private void UntrackKey(string key)
    {
        lock (_cacheKeysLock)
        {
            _cacheKeys.Remove(key);
        }
    }
}
}