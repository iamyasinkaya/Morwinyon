namespace Morwinyon.CustomizedMiddlewares;

/// <summary>
/// This class defines options for restricting access based on IP addresses.
/// </summary>
public class IPRestrictionOptions
{
    public List<string> AllowedIPs { get; set; } = new List<string> { "127.0.0.1", "::1" };
}
