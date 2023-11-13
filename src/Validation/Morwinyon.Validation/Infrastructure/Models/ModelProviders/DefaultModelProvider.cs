using Morwinyon.Validation.Infrastructure.Models.ResponseModels;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace Morwinyon.Validation.Infrastructure.Models.ModelProviders;

/// <inheritdoc/>
internal class DefaultModelProvider : IDefaultModelProvider
{

    /// <inheritdoc/>
    public object GetModel(ValueEnumerable modelStateValues)
    {
        var errors = modelStateValues.Where(i => i.Errors.Count > 0)
            .SelectMany(i => i.Errors)
            .Select(i => i.ErrorMessage);

        return new DefaultValidationErrorResponseModel(errors);
    }
}
