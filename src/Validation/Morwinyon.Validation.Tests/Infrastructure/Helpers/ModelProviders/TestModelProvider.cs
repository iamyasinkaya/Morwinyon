using Microsoft.AspNetCore.Mvc.ModelBinding;
using Morwinyon.Validation.Infrastructure.Models.ModelProviders;
using Morwinyon.Validation.Tests.Infrastructure.Models.ResponseModels;
using System.Net;

namespace Morwinyon.Validation.Tests.Infrastructure.Helpers.ModelProviders;

internal class TestModelProvider : IDefaultModelProvider
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
