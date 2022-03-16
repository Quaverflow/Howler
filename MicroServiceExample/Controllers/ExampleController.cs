using ExamplesForWiseUp.Database;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExample.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExampleController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(Person dto) => Ok();
}