using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Services.Implementations;
using ExamplesForWiseUp.Services.Interfaces;
using Howler;
using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _exampleService;
    private readonly IHowler _howler;

    public ExampleController( IHowler howler, IExampleService exampleService)
    {
        _howler = howler;
        _exampleService = exampleService;
    }

    [HttpPost]
    public async Task<IActionResult> SavePerson([FromBody] Dto dto)
    {
        await _exampleService.SavePerson(dto);
        Cleanup();
        return Ok();
    }
    private void Cleanup()
    {
        FakesRepository.Logs.Clear();
        FakesRepository.EmailsSent.Clear();
        FakesRepository.SmsSent.Clear();
    }
}