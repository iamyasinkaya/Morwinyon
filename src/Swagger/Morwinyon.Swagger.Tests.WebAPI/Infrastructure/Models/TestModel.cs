using System.Text.Json.Serialization;

namespace Morwinyon.Swagger.Tests.WebAPI.Infrastructure.Models;

public class TestModel
{
    public int Id { get; set; }

    public string FullName { get; set; }

    [JsonIgnore]
    public int Age { get; set; }
}