using Microsoft.AspNetCore.Mvc;

namespace Morwinyon.Caching.Tests.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    
    private readonly ICacheService<string> _cacheService;

    public TestController( ICacheService<string> cacheService)
    {
        _cacheService = cacheService;
    }

    [HttpGet]
    public IActionResult GetFromCache()
    {
      
        string key = _cacheService.Get("cacheKey");

        if (key is not null)
            return Ok(key);
        return BadRequest(new string[]
        {
            $"TraceId: {Guid.NewGuid()}",
            $"RequestId: {Guid.NewGuid()}"
        });
    }

    [HttpPost]
    public IActionResult SetFromCache([FromBody] string cacheKey)
    {
        _cacheService.Set("cacheKey", cacheKey,TimeSpan.FromMinutes(5));

        return Ok();
    }
}
