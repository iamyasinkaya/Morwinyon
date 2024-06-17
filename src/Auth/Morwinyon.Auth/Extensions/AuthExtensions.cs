using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Morwinyon.Auth;

public static class AuthExtensions
{

    /// <summary>
    /// Adds JWT Bearer authentication to the IServiceCollection with default options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add JWT Bearer authentication to.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        var defaultOptions = new JwtBearerOptions();
        return services.AddJwtAuthentication(defaultOptions);
    }

    /// <summary>
    /// Adds JWT Bearer authentication to the IServiceCollection with custom options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add JWT Bearer authentication to.</param>
    /// <param name="options">The JwtBearerOptions instance containing configuration for JWT Bearer authentication.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtBearerOptions options)
    {
        var keyBytes = Encoding.ASCII.GetBytes(options.Key);

        services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = options.ValidateIssuer,
                ValidateAudience = options.ValidateAudience,
                ValidateLifetime = options.ValidateLifetime,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ClockSkew = options.ClockSkew,
                RoleClaimType = options.RoleClaimType,
                NameClaimType = options.NameClaimType
            };

            jwtOptions.SaveToken = options.SaveToken;

            if (!string.IsNullOrEmpty(options.MetadataAddress))
            {
                jwtOptions.MetadataAddress = options.MetadataAddress;
            }

            jwtOptions.RequireHttpsMetadata = options.RequireHttpsMetadata;
        });

        return services;
    }
    /// <summary>
    /// Adds OAuth 2.0 with OpenID Connect authentication to the IServiceCollection with default options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add OAuth 2.0 with OpenID Connect authentication to.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    public static IServiceCollection AddOAuth2OpenIDConnectAuthentication(this IServiceCollection services)
    {
        var defaultOptions = new OAuth2Options();
        return services.AddOAuth2OpenIDConnectAuthentication(defaultOptions);
    }

    /// <summary>
    /// Adds OAuth 2.0 with OpenID Connect authentication to the IServiceCollection with custom options.
    /// </summary>
    /// <param name="services">The IServiceCollection to add OAuth 2.0 with OpenID Connect authentication to.</param>
    /// <param name="options">The OAuth2Options instance containing configuration for OAuth 2.0 with OpenID Connect authentication.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    public static IServiceCollection AddOAuth2OpenIDConnectAuthentication(this IServiceCollection services, OAuth2Options options)
    {
        services.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            authenticationOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            authenticationOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddOpenIdConnect(openIdConnectOptions =>
        {
            openIdConnectOptions.ClientId = options.ClientId;
            openIdConnectOptions.ClientSecret = options.ClientSecret;
            openIdConnectOptions.Authority = options.Authority;
            openIdConnectOptions.ResponseType = options.ResponseType;
            openIdConnectOptions.SaveTokens = options.SaveTokens;
            openIdConnectOptions.RequireHttpsMetadata = options.RequireHttpsMetadata;
        });

        return services;
    }

    /// <summary>
    /// Adds custom authentication to the IServiceCollection based on the specified AuthenticationOptions.
    /// </summary>
    /// <param name="services">The IServiceCollection to add custom authentication to.</param>
    /// <param name="options">The AuthenticationOptions instance containing the desired authentication scheme and configuration.</param>
    /// <returns>The IServiceCollection instance, allowing for method chaining.</returns>
    /// <exception cref="ArgumentException">Thrown if the specified authentication scheme is not supported.</exception>
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, AuthenticationOptions options)
    {
        switch (options.Scheme)
        {
            case AuthenticationSchemes.JwtBearer:
                services.AddJwtAuthentication(options.JwtBearerOptions);
                break;
            case AuthenticationSchemes.OAuth2:
                services.AddOAuth2OpenIDConnectAuthentication(options.OAuth2Options);
                break;
            default:
                throw new ArgumentException("Unsupported authentication scheme");
        }

        return services;
    }

}

