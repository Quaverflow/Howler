using Howler;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Services;
using HowlerExamples.Structures.Base;
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
        var data = _howler.Invoke(() => _serviceUsingHowler.GetData(), StructuresIds.Get);
        var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
        FakesRepository.Logs.Clear();
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetMoreDataHowler()
    {
        var data = _howler.Invoke(() => _serviceUsingHowler.GetMoreData(), StructuresIds.Get);
        var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
        FakesRepository.Logs.Clear();
        return Ok(result);
    }


    [HttpPost]
    public IActionResult PostDataHowler([FromBody] Dto dto)
    {
        _howler.InvokeVoid(() => _serviceUsingHowler.PostData(dto), StructuresIds.Post, dto);
        var result = string.Join("\n", FakesRepository.Logs);
        FakesRepository.Logs.Clear();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult PostDataHowlerAndNotify([FromBody] DtoNotifiable dto)
    {
        _howler.InvokeVoid(() => _serviceUsingHowler.PostDataAndNotify(dto), StructuresIds.PostAndNotify, dto);

        var result = string.Join("\n", FakesRepository.Logs);
        result += "\n" + string.Join("\n", FakesRepository.EmailsSent);
        result +=" \n" +  string.Join("\n", FakesRepository.SmsSent);
        FakesRepository.Logs.Clear();
        FakesRepository.EmailsSent.Clear();
        FakesRepository.SmsSent.Clear();

        return Ok(result);
    }
}