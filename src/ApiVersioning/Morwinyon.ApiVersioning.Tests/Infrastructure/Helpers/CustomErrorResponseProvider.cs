﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Morwinyon.ApiVersioning.Tests.Infrastructure.Models;

namespace Morwinyon.ApiVersioning.Tests.Infrastructure.Helpers;

internal class CustomErrorResponseProvider : IErrorResponseProvider
{
    public IActionResult CreateResponse(ErrorResponseContext context)
    {
        var response = new CustomErrorResponseModel()
        {
            ErrorCode = context.ErrorCode,
            ErrorMessage = "ApiVersioning Exception",
            ErrorDetail = context.Message
        };

        var result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };

        return result;
    }
}