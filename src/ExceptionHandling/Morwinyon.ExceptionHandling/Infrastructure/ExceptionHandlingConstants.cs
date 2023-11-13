using System.Text.Json;

namespace Morwinyon.AspNetCore.ExceptionHandling;

internal class ExceptionHandlingConstants
{
    internal const string DefaultExceptionMessage = "Internal server error occured!";

    internal const string DefaultResponseContentType = "application/json";

    internal static JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
}
