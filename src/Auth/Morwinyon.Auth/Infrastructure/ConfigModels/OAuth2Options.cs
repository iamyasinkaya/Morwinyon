using System.Security.Cryptography;

namespace Morwinyon.Auth;

/// <summary>
/// Represents configuration options for OAuth 2.0 authentication.
/// </summary>
public class OAuth2Options
{
    /// <summary>
    /// Default constructor that generates a secure random ClientId and ClientSecret.
    /// </summary>
    public OAuth2Options()
    {
        ClientId = GenerateSecureRandomString(16);
        ClientSecret = GenerateSecureRandomString(32);
    }

    /// <summary>
    /// The client identifier used for OAuth 2.0 authentication.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// The client secret used for OAuth 2.0 authentication.
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// The authority (issuer) URL for the OAuth 2.0 provider. Defaults to "https://default_authority".
    /// </summary>
    public string Authority { get; set; } = "https://default_authority";

    /// <summary>
    /// The OAuth 2.0 response type. Defaults to "code".
    /// </summary>
    public string ResponseType { get; set; } = "code";

    /// <summary>
    /// Whether to save access and refresh tokens after successful authentication. Defaults to true.
    /// </summary>
    public bool SaveTokens { get; set; } = true;

    /// <summary>
    /// Whether to require HTTPS for metadata retrieval from the OAuth 2.0 provider. Defaults to true.
    /// </summary>
    public bool RequireHttpsMetadata { get; set; } = true;

    /// <summary>
    /// Generates a cryptographically secure random string of the specified length.
    /// </summary>
    /// <param name="length">The desired length of the random string.</param>
    /// <returns>A cryptographically secure random string.</returns>
    private static string GenerateSecureRandomString(int length)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var data = new byte[length];
            rng.GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}
