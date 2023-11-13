using Morwinyon.Validation.Infrastructure.Models.ResponseModels;

namespace Morwinyon.Validation.WebAPI.Infrastructure.Models;

public class TestResponse : BaseValidationErrorResponseModel
{
    public int HttpStatusCode { get; set; }
}
