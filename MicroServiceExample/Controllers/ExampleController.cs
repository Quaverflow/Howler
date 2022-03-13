using ExamplesCore.Database;
using ExamplesCore.Helpers;
using ExamplesCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExample.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExampleController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<string>> Post(Person dto) => await Task.FromResult(Ok(new MicroServiceResult { Response =$"MicroService Called. Received {dto.ToJson()}."}));
}