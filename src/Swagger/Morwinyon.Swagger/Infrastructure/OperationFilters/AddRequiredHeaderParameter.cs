﻿using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Morwinyon.OpenApi;

internal class AddRequiredHeaderParameter : IOperationFilter
{
    private readonly string headerKey;
    private readonly string headerValue;

    public AddRequiredHeaderParameter(string headerKey, string headerValue)
    {
        this.headerKey = headerKey;
        this.headerValue = headerValue;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = headerKey,
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema()
            {
                Type = "string",
                Default = new OpenApiString(headerValue)
            }
        });
    }
}
