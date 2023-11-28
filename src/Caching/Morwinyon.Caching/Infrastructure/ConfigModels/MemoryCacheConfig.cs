using Microsoft.Extensions.Internal;
using System;

namespace Morwinyon.Caching;

/// <summary>
/// Represents the configuration options for an in-memory cache.
/// </summary>
public class MemoryCacheConfig
{
    /// <summary>
    /// Gets or sets the host for the in-memory cache.
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets the port for the in-memory cache.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the password for the in-memory cache (if applicable).
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the maximum size limit for the in-memory cache.
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// Gets or sets the frequency at which the in-memory cache scans for expired items.
    /// </summary>
    public TimeSpan ExpirationScanFrequency { get; set; }

    /// <summary>
    /// Gets or sets the connection string for the in-memory cache.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the compaction percentage for the in-memory cache.
    /// </summary>
    public double CompactionPercentage { get; set; }

    /// <summary>
    /// Gets or sets the system clock for the in-memory cache.
    /// </summary>
    public ISystemClock Clock { get; set; }
}
