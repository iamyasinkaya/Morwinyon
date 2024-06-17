

using Microsoft.AspNetCore.Mvc;

namespace Morwinyon.RateLimiting.Test.WebAPI;

[ApiController]
[Route("[controller]")]
public class ExampleController : ControllerBase
{
    private readonly RateLimiter<string> _rateLimiter;

    public ExampleController(RateLimiter<string> rateLimiter)
    {
        _rateLimiter = rateLimiter;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var clientId = RateLimitHelper.GenerateClientId(
            HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            "Example/Get"
        );

        var rateLimitResult = _rateLimiter.IsRequestAllowed(clientId);

        if (rateLimitResult.Status == RateLimitStatus.Denied)
        {
            Response.Headers["Retry-After"] = rateLimitResult.RetryAfter.TotalSeconds.ToString();
            return StatusCode(429, rateLimitResult.Message);
        }

        Response.Headers["X-RateLimit-Remaining"] = rateLimitResult.RemainingRequests.ToString();
        Response.Headers["X-RateLimit-Reset"] = rateLimitResult.ResetTime.ToString("o");

        return Ok(rateLimitResult.Message);
    }
}