using Microsoft.AspNetCore.Mvc;

namespace Morwinyon.ApiVersioning.Tests.Infrastructure.Controller.v1;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("v1");
    }
}