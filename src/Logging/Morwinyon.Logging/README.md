# Logging

The Logging Extension is designed to enhance your WebAPI Projects by introducing a comprehensive Logging Feature. Notably, it offers configurability and by default, comes pre-configured with optimal settings upon implementation.


The Logging Config incorporates three distinct functions, outlined below, that empower the system to seamlessly retrieve the provided API version from various sources.


## NuGet
| Package Name | Package | Downloads |
| ------------- | ------- | --------- |
| Logging       | [![NuGet](https://img.shields.io/nuget/v/Morwinyon.Logging?style=for-the-badge)](https://www.nuget.org/packages/Morwinyon.Logging) | [![NuGet](https://img.shields.io/nuget/dt/Morwinyon.Logging?style=for-the-badge)](https://www.nuget.org/packages/Morwinyon.Logging/) |


### Installation

```bash
PM> NuGet\Install-Package Morwinyon.Logging
```
or
```bash
dotnet add package Morwinyon.Logging
```

----

## Usage


It could be used like showed below. In this case, version 1.0 will be used as default version when not specified.

```csharp
builder.Services.AddMorwinyonDefaultFileLogging();
builder.Services.AddMorwiyonDefaultConsoleLogging();
builder.Services.AddMorwinyonDefaultDatabaseLogging();

```




```csharp
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // create data
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            // convert to JSON format
            var json = JsonSerializer.Serialize(forecasts);

            // logging example
            _logger.LogInformation("Getting weather forecast data {WeatherForecastsJson}", json);

            return forecasts;
        }
    }

```

----


