namespace Morwinyon.Validation.Tests.Infrastructure.Models.ResponseModels;

internal class TestResponse : BaseValidationErrorResponseModel
{
    public int HttpStatusCode { get; set; }
}
