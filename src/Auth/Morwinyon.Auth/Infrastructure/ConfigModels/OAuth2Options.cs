using System.Security.Cryptography;

namespace Morwinyon.Auth;

public class OAuth2Options
{
    // Varsayılan clientId ve clientSecret oluşturucu
    public OAuth2Options()
    {
        ClientId = GenerateSecureRandomString(16);
        ClientSecret = GenerateSecureRandomString(32);
    }

    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Authority { get; set; } = "https://default_authority";
    public string ResponseType { get; set; } = "code";
    public bool SaveTokens { get; set; } = true;
    public bool RequireHttpsMetadata { get; set; } = true;

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