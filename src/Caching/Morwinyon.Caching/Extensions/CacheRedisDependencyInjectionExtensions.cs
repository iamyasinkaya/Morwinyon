using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;

namespace Morwinyon.Caching;

/// <summary>
/// 
/// </summary>
public static class CacheRedisDependencyInjectionExtensions
{
    /// <summary>
    /// Adds Morwinyon Redis Cache to the IServiceCollection with default configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddMorwinyonRedisCache(this IServiceCollection services)
    {
        ValidateServiceCollection(services);

        // Configure Redis connection
        var redisConfiguration = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },  // Redis server address and port
            ConnectTimeout = 5000,              // Connection timeout (ms)
            SyncTimeout = 1000,                   // Synchronization timeout (ms)
            AbortOnConnectFail = false,         // Abort on connection failure

            // Password for Redis server (if required)
            Password = "your_password",

            // Set the database to be used (default is 0)
            DefaultDatabase = 0,

            // Enable SSL to encrypt the connection
            Ssl = false,
            SslHost = "localhost",  // Set the SSL host if different from the Redis server host

            // Specify the client name to be used in monitoring and logging
            ClientName = "your_client_name",

            // Configure proxy (e.g., for connecting to Redis through a proxy)
            Proxy = Proxy.Twemproxy,  // Replace with your specific proxy type if needed
            
            // Configure high availability and failover options (if applicable)
            // For example, using Redis Sentinel
            // Note: Replace with your specific Sentinel configuration if needed
            // Sentinels = { "sentinel1:26379", "sentinel2:26379", "sentinel3:26379" },
            // ServiceName = "mymaster",

            // Set various timeouts for operations
            // Note: Default is -1 (infinite), adjust as needed
            ConnectRetry = 3,
            CheckCertificateRevocation = false,
            ReconnectRetryPolicy = new ExponentialRetry(500, 1000),

        };
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration));
        services.AddTransient(typeof(ICacheService<>), typeof(RedisCacheService<>));

        return services;
    }

    /// <summary>
    /// Adds Morwinyon Redis Cache to the IServiceCollection with custom configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection to configure.</param>
    /// <param name="options">Action to configure cache options.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddMorwinyonRedisCache(this IServiceCollection services, Action<CacheOptions> options)
    {
        ValidateServiceCollection(services);

        ValidateConfigureOptions(options);

        var config = new CacheOptions();
        options.Invoke(config);

      

         // Configure Redis connection with custom options
         var redisConfiguration = new ConfigurationOptions
        {
            EndPoints = { $"{config.RedisConfig.Endpoints}" },                           // Redis server address and port
            Password = config.RedisConfig.Password,                                       // Redis password (if any)
            ConnectTimeout = config.RedisConfig.ConnectTimeout,                  // Connection timeout (ms)
            SyncTimeout = config.RedisConfig.SyncTimeout,                             // Synchronization timeout (ms)
            AbortOnConnectFail = config.RedisConfig.AbortOnConnectFail      // Abort on connection failure
        };

        // Add Redis connection
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration));
        services.AddTransient(typeof(ICacheService<>), typeof(RedisCacheService<>));

        return services;
    }

    /// <summary>
    /// Validates the IServiceCollection to ensure it is not null.
    /// </summary>
    /// <param name="services">The IServiceCollection to validate.</param>
    private static void ValidateServiceCollection(IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
    }

    /// <summary>
    /// Validates the configure options action to ensure it is not null.
    /// </summary>
    /// <param name="configureOptions">The action to configure cache options.</param>
    private static void ValidateConfigureOptions(Action<CacheOptions> configureOptions)
    {
        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }
    }

}







