using Microsoft.AspNetCore.Mvc;
using Morwinyon.TestCommon.Tests.Common.Constants;

namespace Morwinyon.ExceptionHandling.Tests.Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class TestController : ControllerBase
{
    [HttpGet()]
    public ActionResult Get()
    {
        return Ok("V1");
    }

    [HttpGet("ThrowException")]
    public ActionResult ThrowException()
    {
        throw new Exception(TestConstants.ExceptionMessage);
    }

    [HttpGet("ThrowCustomException")]
    public ActionResult ThrowCustomException()
    {
        throw new InvalidOperationException();
    }
}
