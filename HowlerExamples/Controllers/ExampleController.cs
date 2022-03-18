using System.Runtime.InteropServices;
using ExamplesForWiseUp.Database;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Services.Interfaces;
using ExamplesForWiseUp.Structures;
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
        var result = await _howler.InvokeAsync(StructureIds.Post, 
            ()=> _exampleService.SavePerson(dto), ExampleDbContext.AuthorizedPersonId);

        var response = $"\n||Logs: {string.Join("\n||", FakesRepository.Logs)}\n";
        response += $"\n||Emails: {string.Join("\n", FakesRepository.EmailsSent)}\n";
        response += $"\n||Sms: {string.Join("\n", FakesRepository.SmsSent)}\n";
        response += $"\n||Result: {result.ToJson()}\n";
        FakesRepository.Cleanup();
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _howler.InvokeAsync(StructureIds.Get,
            () => _exampleService.Get());
       
        var response = $"\n||Logs: {string.Join("\n||", FakesRepository.Logs)}\n";
        response += $"\n||Result: {result.ToJson()}\n";

        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Try([FromBody] Dto dto)
    {
        var result = await _exampleService.Try(dto);

        return Ok(result);
    }



}