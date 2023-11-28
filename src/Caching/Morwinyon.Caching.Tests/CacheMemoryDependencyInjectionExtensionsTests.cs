using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Morwinyon.Caching;

[TestFixture]
public class CacheMemoryDependencyInjectionExtensionsTests
{

    [Test]
    public void AddMorwiyonMemoryCache_WhenCalled_ShouldAddDistributedMemoryCacheAndMemoryCache()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddMorwiyonMemoryCache();

        // Assert
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(IDistributedCache)));
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(IMemoryCache)));
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(ICacheService<>)));
    }

    [Test]
    public void AddMorwinyonMemoryCache_WithNullServiceCollection_ShouldThrowArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwiyonMemoryCache());
    }

    [Test]
    public void AddMorwinyonMemoryCache_WithNullConfigureOptions_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<CacheOptions> configureOptions = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwinyonMemoryCache(configureOptions));
    }

    [Test]
    public void RegisterMemoryCacheServices_WhenCalled_ShouldRegisterMemoryCacheAndMemoryCacheService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        CacheMemoryDependencyInjectionExtensions.AddMorwiyonMemoryCache(services);

        // Assert
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(IMemoryCache)));
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(ICacheService<>)));
    }

    [Test]
    public void AddMorwinyonMemoryCache_WithOptions_WhenConfigureOptionsIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<CacheOptions> configureOptions = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwinyonMemoryCache(configureOptions));
    }


    [Test]
    public void AddMorwiyonMemoryCache_WithOptions_WithInvalidConfigureOptions_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<CacheOptions> configureOptions = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwinyonMemoryCache(configureOptions));
    }

    [Test]
    public void AddMorwiyonMemoryCache_WithInvalidServiceCollection_ShouldThrowArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwiyonMemoryCache());
    }


    [Test]
    public void AddMorwiyonMemoryCache_WithDefaultOptions_ShouldUseDefaultDistributedMemoryCacheOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddMorwiyonMemoryCache();

        // Assert
        var distributedMemoryCacheDescriptor = services.SingleOrDefault(desc => desc.ServiceType == typeof(IDistributedCache));
        Assert.IsNotNull(distributedMemoryCacheDescriptor);


    }

    
}