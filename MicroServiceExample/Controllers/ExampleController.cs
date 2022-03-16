
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExample.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExampleController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Post(Person dto) => await Task.FromResult(Ok(new MicroServiceResult { Response =$"MicroService Called. Received {dto.ToJson()}."}));
}