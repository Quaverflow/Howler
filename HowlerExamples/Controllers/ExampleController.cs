using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services;
using ExamplesCore.Structures.Base;
using Howler;
using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ExampleController : ControllerBase
{
    private readonly IServiceUsingHowler _serviceUsingHowler;
    private readonly IHowler _howler;

    public ExampleController(IServiceUsingHowler serviceUsingHowler, IHowler howler)
    {
        _serviceUsingHowler = serviceUsingHowler;
        _howler = howler;
    }

    public async Task<IActionResult> PostDataHowlerAndNotify([FromBody] DtoNotifiable dto)
    {
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