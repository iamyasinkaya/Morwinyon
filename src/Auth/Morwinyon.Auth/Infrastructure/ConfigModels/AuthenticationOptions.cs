namespace Morwinyon.Auth;

public class AuthenticationOptions
{
    public AuthenticationSchemes Scheme { get; set; }
    public JwtBearerOptions JwtBearerOptions { get; set; }
    public OAuth2Options OAuth2Options { get; set; }
}