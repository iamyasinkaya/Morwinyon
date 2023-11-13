using Microsoft.AspNetCore.Mvc;

namespace Morwinyon.ApiVersioning.Tests.Infrastructure.Controller.v2;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("2.0")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("V2");
    }
}
