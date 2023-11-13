using Microsoft.Extensions.DependencyInjection;
using Morwinyon.Validation.Infrastructure.Models.ModelProviders;
using Morwinyon.Validation.Tests.Infrastructure.Helpers.ModelProviders;
using Morwinyon.Validation.Tests.Infrastructure.Helpers.Validators;
using Morwinyon.Validation.Extensions;
using System.Reflection;

namespace Morwinyon.Validation.Tests.Tests;

public sealed class ValidationDIExtensionsTests
{
    #region AddMorwinyonValidatorFromAssemblyContaining Tests

    [Test]
    public void AddValidatorFromAssemblyContaining_ShouldRegisterSpecificValidator()
    {
        // Arrange
        var services = new ServiceCollection();

        // Action
        services.AddMorwinyonValidatorFromAssemblyContaining<TestValidator>();

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();

        // Assert
        validator.Should().NotBeNull();
    }

    [Test]
    public void AddValidatorFromAssemblyContaining_ShouldRegisterAllValidators()
    {
        // Arrange
        var services = new ServiceCollection();

        // Action
        services.AddMorwinyonValidatorFromAssemblyContaining<TestValidator>();

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();
        var validator2 = provider.GetService<TestValidatorV2>();

        // Assert
        validator.Should().NotBeNull();
        validator2.Should().NotBeNull();
    }

    #endregion

    #region AddMorwinyonValidator Tests

    [Test]
    public void AddValidator_WithNoAssembly_ShouldRegisterSpecificValidator()
    {
        // Arrange
        var services = new ServiceCollection();

        // Action
        services.AddMorwinyonValidator();

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();

        // Assert
        validator.Should().NotBeNull();
    }

    [Test]
    public void AddValidator_WithNoAssembly_ShouldRegisterAllValidator()
    {
        // Arrange
        var services = new ServiceCollection();

        // Action
        services.AddMorwinyonValidator();

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();
        var validator2 = provider.GetService<TestValidatorV2>();

        // Assert
        validator.Should().NotBeNull();
        validator2.Should().NotBeNull();
    }

    [Test]
    public void AddValidator_WithAssembly_ShouldRegisterSpecificValidator()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();

        // Action
        services.AddMorwinyonValidatorFromAssembly(assembly);

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();

        // Assert
        validator.Should().NotBeNull();
    }

    [Test]
    public void AddValidator_WithAssembly_ShouldRegisterAllValidator()
    {
        // Arrange
        var services = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();

        // Action
        services.AddMorwinyonValidatorFromAssembly(assembly);

        var provider = services.BuildServiceProvider();
        var validator = provider.GetService<TestValidator>();
        var validator2 = provider.GetService<TestValidatorV2>();

        // Assert
        validator.Should().NotBeNull();
        validator2.Should().NotBeNull();
    }

    [Test]
    public void AddValidatorWithConfig_WithNoAssembly_ShouldRegisterSpecificValidator()
    {
        // Arrange
        var services = new ServiceCollection();

        // Action
        services.AddMorwinyonValidator(conf =>
        {
            conf.UseModelProvider<TestModelProvider>();
        });

        var provider = services.BuildServiceProvider();
        var modelProvider = provider.GetService<IDefaultModelProvider>();

        // Assert
        modelProvider.Should().BeOfType<TestModelProvider>();
    }

    #endregion
}
