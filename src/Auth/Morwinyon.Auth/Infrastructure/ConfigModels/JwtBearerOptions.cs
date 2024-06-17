using System.Security.Cryptography;

namespace Morwinyon.Auth;

public class JwtBearerOptions
{
    // Varsayılan key, issuer ve audience oluşturucu
    public JwtBearerOptions()
    {
        Key = GenerateSecureRandomString(32);
        Issuer = GenerateSecureRandomString(16);
        Audience = GenerateSecureRandomString(16);
    }

    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateIssuerSigningKey { get; set; } = true;
    public TimeSpan ClockSkew { get; set; } = TimeSpan.Zero;
    public TimeSpan TokenExpiration { get; set; } = TimeSpan.FromMinutes(60);
    public bool SaveToken { get; set; } = true;
    public string RoleClaimType { get; set; } = "role";
    public string NameClaimType { get; set; } = "name";
    public bool RequireHttpsMetadata { get; set; } = true;
    public string MetadataAddress { get; set; }

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