using System.Text.Json;

namespace Morwinyon.TestCommon.Tests.Common.Constants;

public sealed class GeneralConstants
{
    public static JsonSerializerOptions JsonOptions { get; } = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
}
