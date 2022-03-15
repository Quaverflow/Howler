using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services;
using ExamplesCore.Structures.Base;
using ExamplesCore.Structures.StructureDtos;
using Howler;
using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class HowlerServiceExamplesController : ControllerBase
{
    private readonly IServiceUsingHowler _serviceUsingHowler;
    private readonly IHowler _howler;

    public HowlerServiceExamplesController(IServiceUsingHowler serviceUsingHowler, IHowler howler)
    {
        _serviceUsingHowler = serviceUsingHowler;
        _howler = howler;
    }



    [HttpGet]
    public IActionResult GetDataHowler()
    {
        Cleanup();
        var data = _howler.Invoke(() => _serviceUsingHowler.GetData(), StructuresIds.Get);
        var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetMoreDataHowler()
    {
        Cleanup();
        var data = _howler.Invoke(() => _serviceUsingHowler.GetMoreData(), StructuresIds.Get);
        var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
        return Ok(result);
    }


    [HttpPost]
    public IActionResult PostDataHowler([FromBody] Dto dto)
    {
        Cleanup();
        _howler.InvokeVoid(() => _serviceUsingHowler.PostData(dto), StructuresIds.Post, dto);
        var result = string.Join("\n", FakesRepository.Logs);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostDataHowlerAndNotify([FromBody] DtoNotifiable dto)
    {
        Cleanup();
        var savedEntity = await _howler.Invoke(async () => await _serviceUsingHowler.PostDataAndNotify(dto), StructuresIds.PostAndNotify, dto);

        var result = string.Join("\n", FakesRepository.Logs);
        result += "\n" + string.Join("\n", FakesRepository.EmailsSent);
        result +=" \n" +  string.Join("\n", FakesRepository.SmsSent);
        result += $"\nsaved entity: {savedEntity.Data}";

        return Ok(result);
    }
    private void Cleanup()
    {
        FakesRepository.Logs.Clear();
        FakesRepository.EmailsSent.Clear();
        FakesRepository.SmsSent.Clear();
    }
}