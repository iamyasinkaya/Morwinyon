using Microsoft.AspNetCore.Mvc.ModelBinding;
using Morwinyon.Validation.Infrastructure.Models.ModelProviders;
using Morwinyon.Validation.WebAPI.Infrastructure.Models;
using System.Net;

namespace Morwinyon.Validation.WebAPI.Infrastructure;

public class TestModelProvider : IDefaultModelProvider
{
    public object GetModel(ModelStateDictionary.ValueEnumerable modelStateValues)
    {
        return new TestResponse()
        {
            Errors = modelStateValues.SelectMany(i => i.Errors).Select(i => string.Join(Environment.NewLine, i.ErrorMessage)).ToList(),
            HttpStatusCode = (int)HttpStatusCode.BadRequest
        };
    }
}