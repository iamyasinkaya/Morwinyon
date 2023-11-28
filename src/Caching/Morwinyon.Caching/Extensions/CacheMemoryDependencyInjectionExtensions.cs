using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Morwinyon.Caching
{

    /// <summary>
    /// Extension class to configure and add a custom in-memory cache
    /// </summary>
    public static class CacheMemoryDependencyInjectionExtensions
    {
        /// <summary>
        /// Extension method to configure and add a custom in-memory cache service to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure.</param>
        /// <returns>The modified IServiceCollection.</returns>
        public static IServiceCollection AddMorwiyonMemoryCache(this IServiceCollection services)
        {
            ValidateServiceCollection(services);

            // Configure and add distributed memory cache with specific options
            services.AddDistributedMemoryCache(opt =>
            {
                opt.SizeLimit = 1024;
                opt.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
                opt.CompactionPercentage = 0.20;
            });

            // Add the default in-memory cache
            services.AddMemoryCache();

            RegisterMemoryCacheServices(services);

            return services;
        }


        /// <summary>
        /// Extension method to configure and add a custom in-memory cache service to the IServiceCollection using specified options.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure.</param>
        /// <param name="options">Action to configure cache options.</param>
        /// <returns>The modified IServiceCollection.</returns>
        public static IServiceCollection AddMorwinyonMemoryCache(this IServiceCollection services, Action<CacheOptions> options)
        {
            ValidateServiceCollection(services);

            ValidateConfigureOptions(options);

            var config = new CacheOptions();
            options.Invoke(config);

            ConfigureDistributedMemoryCache(services, config);

            services.AddMemoryCache();

            RegisterMemoryCacheServices(services);

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

        /// <summary>
        /// Configures the distributed memory cache with the specified options.
        /// </summary>
        /// <param name="services">The IServiceCollection to configure.</param>
        /// <param name="config">Cache configuration options.</param>
        private static void ConfigureDistributedMemoryCache(IServiceCollection services, CacheOptions config)
        {
            services.AddDistributedMemoryCache(opt =>
            {
                opt.SizeLimit = config.MemoryConfig.SizeLimit;
                opt.ExpirationScanFrequency = config.MemoryConfig.ExpirationScanFrequency;
                opt.CompactionPercentage = config.MemoryConfig.CompactionPercentage;
            });
        }

        /// <summary>
        /// Registers the MemoryCache and MemoryCacheService in the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to register services.</param>
        private static void RegisterMemoryCacheServices(IServiceCollection services)
        {
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddTransient(typeof(ICacheService<>), typeof(MemoryCacheService<>));
        }
    }
}