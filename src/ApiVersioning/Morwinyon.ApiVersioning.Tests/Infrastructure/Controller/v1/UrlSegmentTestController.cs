using Microsoft.AspNetCore.Mvc;
using Morwinyon.TestCommon.Tests.Common.Constants;

namespace Morwinyon.ApiVersioning.Tests.Infrastructure.Controller.v1;

[ApiController]
[Route(TestConstants.ControllerRoute)]
[ApiVersion("1.0")]
public sealed class UrlSegmentTestController : ControllerBase
{
    [HttpGet()]
    public ActionResult Get()
    {
        return Ok("V1");
    }
}
