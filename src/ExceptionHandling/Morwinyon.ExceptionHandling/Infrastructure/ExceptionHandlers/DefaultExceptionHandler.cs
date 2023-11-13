using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Morwinyon.AspNetCore.ExceptionHandling;

internal class DefaultExceptionHandler
{
    internal static async Task Handle(HttpContext context,
                                        Exception exception,
                                        ILogger logger,
                                        bool useExceptionDetails = false)
    {

        var res = new DefaultExceptionHandlerResponseModel()
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Detail = useExceptionDetails
                        ? exception.ToString()
                        : ExceptionHandlingConstants.DefaultExceptionMessage
        };

        logger?.LogError(exception, exception.ToString());
        await context.WriteResponseAsync(res, res.StatusCode);
    }
}
