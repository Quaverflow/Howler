using HowlerExamples.Models;
using HowlerExamples.Services;
using HowlerExamples.Structures;
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

        public HowlerExamplesController(INormalService normalService, IServiceUsingHowler serviceUsingHowler, IFakeLogger logger)
        {
            _normalService = normalService;
            _serviceUsingHowler = serviceUsingHowler;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetDataNormal()
        {
            var data = _normalService.GetData();
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }     
        
        [HttpGet]
        public IActionResult GetDataHowler()
        {
            var data = _serviceUsingHowler.GetData();
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }       

        [HttpGet]
        public IActionResult GetMoreDataHowler()
        {
            var data = _serviceUsingHowler.GetMoreData();
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult PostDataHowler([FromBody] Dto dto)
        {
            var data = _serviceUsingHowler.PostData(dto);
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }
    }
}