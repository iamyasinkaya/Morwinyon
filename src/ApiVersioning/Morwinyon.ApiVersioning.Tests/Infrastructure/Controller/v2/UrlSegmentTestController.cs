using Microsoft.AspNetCore.Mvc;
using Morwinyon.TestCommon.Tests.Common.Constants;

namespace Morwinyon.ApiVersioning.Tests.Infrastructure.Controller.v2;

[ApiController]
[Route(TestConstants.ControllerRoute)]
[ApiVersion("2.0")]
public sealed class UrlSegmentTestController : ControllerBase
{
    [HttpGet()]
    public ActionResult Get()
    {
        return Ok("V2");
    }
}
