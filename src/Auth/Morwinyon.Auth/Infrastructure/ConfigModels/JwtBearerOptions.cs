using System.Security.Cryptography;

namespace Morwinyon.Auth;

/// <summary>
/// Represents configuration options for JWT Bearer authentication.
/// </summary>
public class JwtBearerOptions
{
    /// <summary>
    /// Default constructor that generates secure random strings for Key, Issuer, and Audience.
    /// </summary>
    public JwtBearerOptions()
    {
        Key = GenerateSecureRandomString(32);
        Issuer = GenerateSecureRandomString(16);
        Audience = GenerateSecureRandomString(16);
    }

    /// <summary>
    /// The secret key used for signing and validating JWT tokens.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// The issuer (who created the token) of the JWT token.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// The intended recipient(s) of the JWT token.
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// Whether to validate the Audience claim in the JWT token. Defaults to false.
    /// </summary>
    public bool ValidateAudience { get; set; } = false;

    /// <summary>
    /// Whether to validate the expiration time (Lifetime) claim in the JWT token. Defaults to true.
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    /// Whether to validate the Issuer claim in the JWT token. Defaults to true.
    /// </summary>
    public bool ValidateIssuer { get; set; } = true;

    /// <summary>
    /// Whether to validate the issuer's signature on the JWT token. Defaults to true.
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; } = true;

    /// <summary>
    /// The allowed clock skew (time difference) between the server and the token issuer. Defaults to TimeSpan.Zero.
    /// </summary>
    public TimeSpan ClockSkew { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// The expiration time for newly generated JWT tokens. Defaults to TimeSpan.FromMinutes(60) (1 hour).
    /// </summary>
    public TimeSpan TokenExpiration { get; set; } = TimeSpan.FromMinutes(60);

    /// <summary>
    /// Whether to save the access token after successful authentication. Defaults to true.
    /// </summary>
    public bool SaveToken { get; set; } = true;

    /// <summary>
    /// The claim type used to extract the user's role(s) from the JWT token. Defaults to "role".
    /// </summary>
    public string RoleClaimType { get; set; } = "role";

    /// <summary>
    /// The claim type used to extract the user's name from the JWT token. Defaults to "name".
    /// </summary>
    public string NameClaimType { get; set; } = "name";

    /// <summary>
    /// Whether to require HTTPS for metadata retrieval from the token issuer. Defaults to true.
    /// </summary>
    public bool RequireHttpsMetadata { get; set; } = true;

    /// <summary>
    /// The URL of the metadata endpoint for the token issuer (optional).
    /// </summary>
    public string MetadataAddress { get; set; }

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
