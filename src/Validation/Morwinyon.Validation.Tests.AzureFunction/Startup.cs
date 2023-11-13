using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System;
using TransactionAccess.API;
using Morwinyon.Validation.Extensions;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TransactionAccess.API;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    /// <summary>
    /// Configures the specified builder.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();

        builder.Services.AddMorwinyonValidator();
    }

    /// <summary>
    /// Configures the application configuration.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        builder.ConfigurationBuilder
            .SetBasePath(Environment.CurrentDirectory)
#if DEBUG
            .AddJsonFile("local.settings.json", true, true)
#endif
            .AddEnvironmentVariables();
    }
}
