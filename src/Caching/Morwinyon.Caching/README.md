# Caching

The Caching Extension is designed to enhance your WebAPI Projects by introducing a comprehensive Caching Feature. Notably, it offers configurability and by default, comes pre-configured with optimal settings upon implementation.


The Caching Config incorporates three distinct functions, outlined below, that empower the system to seamlessly retrieve the provided API version from various sources.


## NuGet
| Package Name | Package | Downloads |
| ------------- | ------- | --------- |
| Caching       | [![NuGet](https://img.shields.io/nuget/v/Morwinyon.Caching?style=for-the-badge)](https://www.nuget.org/packages/Morwinyon.Caching) | [![NuGet](https://img.shields.io/nuget/dt/Morwinyon.Caching?style=for-the-badge)](https://www.nuget.org/packages/Morwinyon.Caching/) |


### Installation

```bash
PM> NuGet\Install-Package Morwinyon.Caching
```
or
```bash
dotnet add package Morwinyon.Caching
```

----

## Usage

```csharp
builder.Services.AddMorwinyonRedisCache(config =>
{
    config.RedisConfig.Endpoints = "localhost:6379";
    config.RedisConfig.ConnectTimeout = 5000;
    config.RedisConfig.SyncTimeout = 1000;
    config.RedisConfig.AbortOnConnectFail = false;
});
builder.Services.AddMorwinyonMemoryCache(config =>
{
    config.MemoryConfig.SizeLimit = 1024;
    config.MemoryConfig.ExpirationScanFrequency = TimeSpan.FromMinutes(1);
    config.MemoryConfig.CompactionPercentage = 0.20;
});
```

It could be used like showed below. In this case, version 1.0 will be used as default version when not specified.

```csharp
builder.Services.AddMorwiyonMemoryCache();
builder.Services.AddMorwinyonRedisCache();
```




```csharp
[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    // Injecting ICacheService<string> for caching operations
    private readonly ICacheService<string> _cacheService;

    public TestController(ICacheService<string> cacheService)
    {
        _cacheService = cacheService;
    }

    // HTTP GET method to retrieve data from cache
    [HttpGet]
    public IActionResult GetFromCache()
    {
        // Attempt to retrieve cached data using the key "cacheKey"
        string key = _cacheService.Get("cacheKey");

        // If the data is found in the cache, return it as Ok result; otherwise, return a BadRequest with new trace and request IDs
        if (key is not null)
            return Ok(key);
        return BadRequest(new string[]
        {
            $"TraceId: {Guid.NewGuid()}",
            $"RequestId: {Guid.NewGuid()}"
        });
    }

    // HTTP POST method to set data into the cache
    [HttpPost]
    public IActionResult SetFromCache([FromBody] string cacheKey)
    {
        // Set the provided data into the cache with the key "cacheKey" and a time-to-live of 5 minutes
        _cacheService.Set("cacheKey", cacheKey, TimeSpan.FromMinutes(5));

        // Return Ok result to indicate successful caching
        return Ok();
    }
}

```

----


