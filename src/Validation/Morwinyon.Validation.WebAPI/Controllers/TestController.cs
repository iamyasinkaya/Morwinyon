using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Morwinyon.Validation.WebAPI.Infrastructure.Models;

namespace Morwinyon.Validation.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    //private readonly IValidator<TestModel> validator;

    //public TestController(IValidator<TestModel> validator)
    //{
    //    this.validator = validator;
    //}

    [HttpPost]

    public ActionResult Post([FromBody] TestModel testModel)
    {
        //var result = validator.Validate(testModel);
        //if (!result.IsValid)
        //    return BadRequest(result);

        return Ok(testModel.Name);
    }
}
