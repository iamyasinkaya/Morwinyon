using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Morwinyon.OpenApi.Infrastructure.ProduceResponseType;

/// <summary>
/// The extension class for ResponseTypeModelProviderDependencyInjection
/// </summary>
internal static class ResponseTypeModelProviderDependencyInjection
{
    internal static IServiceCollection ConfigureMorwinyonResponesTypemodelProvider(this IServiceCollection services,
        ResponseTypeModelProviderConfig config)
    {
        services.AddSingleton(config);
        services.AddSingleton<IApplicationModelProvider, ProduceResponseTypeModelProvider>();

        return services;
    }
}
