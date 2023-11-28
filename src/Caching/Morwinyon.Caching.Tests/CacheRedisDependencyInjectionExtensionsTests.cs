using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StackExchange.Redis;

namespace Morwinyon.Caching;

[TestFixture]
public class CacheRedisDependencyInjectionExtensionsTests
{
    [Test]
    public void AddMorwiyonRedisCache_WhenCalled_ShouldAddDistributedRedisCache()
    {
        //Arrange
        var services = new ServiceCollection();

        //Act
        services.AddMorwinyonRedisCache();

        // Assert
        Assert.IsTrue(services.Any(descriptor => descriptor.ServiceType == typeof(ICacheService<>)));
    }

    [Test]
    public void AddMorwinyonRedisCache_WithNullServiceCollection_ShouldThrowArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwinyonRedisCache());
    }

    [Test]
    public void AddMorwinyonRedisCache_WithOptions_WhenConfigureOptionsIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var services = new ServiceCollection();
        Action<CacheOptions> configureOptions = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => services.AddMorwinyonRedisCache(configureOptions));
    }

 
}
