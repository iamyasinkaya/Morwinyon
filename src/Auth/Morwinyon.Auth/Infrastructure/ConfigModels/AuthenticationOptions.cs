namespace Morwinyon.Auth;

/// <summary>
/// Represents configuration options for authentication.
/// </summary>
public class AuthenticationOptions
{
    /// <summary>
    /// The authentication scheme to be used.
    /// </summary>
    public AuthenticationSchemes Scheme { get; set; }

    /// <summary>
    /// Options for JWT Bearer authentication (if applicable).
    /// </summary>
    public JwtBearerOptions JwtBearerOptions { get; set; }

    /// <summary>
    /// Options for OAuth 2.0 authentication (if applicable).
    /// </summary>
    public OAuth2Options OAuth2Options { get; set; }
}
