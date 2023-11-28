using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Morwinyon.Caching;

    [TestFixture]
    public class GeneralTests
{
    [Test]
    public void ValidateServiceCollection_WithNullServiceCollection_ShouldThrowArgumentNullException()
    {
        // Arrange
        IServiceCollection services = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ValidateServiceCollection(services));
    }

    [Test]
    public void ValidateConfigureOptions_WithNullConfigureOptions_ShouldThrowArgumentNullException()
    {
        // Arrange
        Action<CacheOptions> configureOptions = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ValidateConfigureOptions(configureOptions));
    }

    #region Private Methods
    private static void ValidateServiceCollection(IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
    }

    private static void ValidateConfigureOptions(Action<CacheOptions> configureOptions)
    {
        if (configureOptions == null)
        {
            throw new ArgumentNullException(nameof(configureOptions));
        }
    }

    #endregion
}

