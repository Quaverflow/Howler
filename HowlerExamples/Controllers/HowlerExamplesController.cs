using Howler;
using HowlerExamples.CrossCuttingConcerns;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Services;
using HowlerExamples.Structures.Base;
using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HowlerExamplesController : ControllerBase
    {
        private readonly INormalService _normalService;
        private readonly IServiceUsingHowler _serviceUsingHowler;
        private readonly IFakeLogger _logger;
        private readonly IHowler _howler;

        public HowlerExamplesController(INormalService normalService, IServiceUsingHowler serviceUsingHowler, IFakeLogger logger, IHowler howler)
        {
            _normalService = normalService;
            _serviceUsingHowler = serviceUsingHowler;
            _logger = logger;
            _howler = howler;
        }

        [HttpGet]
        public IActionResult GetDataNormal()
        {
            var data = _normalService.GetData();
            var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
            FakesRepository.Logs.Clear();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetDataHowler()
        {
            var data = _howler.Invoke(() => _serviceUsingHowler.GetData(), StructuresIds.GetStructureId);
            var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
            FakesRepository.Logs.Clear();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetMoreDataHowler()
        {
            var data = _howler.Invoke(() => _serviceUsingHowler.GetMoreData(), StructuresIds.GetStructureId);
            var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
            FakesRepository.Logs.Clear();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult PostDataHowler([FromBody] Dto dto)
        {
            _howler.InvokeVoid(() => _serviceUsingHowler.PostData(dto), StructuresIds.PostStructureId, dto);
            var result = string.Join("\n", FakesRepository.Logs);
            FakesRepository.Logs.Clear();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostDataHowlerAndNotify([FromBody] DtoNotifiable dto)
        {
            _howler.InvokeVoid(() => _serviceUsingHowler.PostDataAndNotify(dto), StructuresIds.PostAndNotifyStructureId, dto);
            var result = string.Join("\n", FakesRepository.Logs);
            result += "\n" + string.Join("\n", FakesRepository.EmailsSent);
            result +=" \n" +  string.Join("\n", FakesRepository.SmsSent);
            FakesRepository.Logs.Clear();
            FakesRepository.EmailsSent.Clear();
            FakesRepository.SmsSent.Clear();

            return Ok(result);
        }
    }
}