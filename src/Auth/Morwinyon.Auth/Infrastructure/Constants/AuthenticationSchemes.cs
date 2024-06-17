namespace Morwinyon.Auth;

public enum AuthenticationSchemes
{
    /// <summary>
    /// Indicates authentication using JSON Web Tokens (JWT).
    /// </summary>
    JwtBearer,

    /// <summary>
    /// Indicates authentication using the OAuth 2.0 protocol.
    /// </summary>
    OAuth2
}
