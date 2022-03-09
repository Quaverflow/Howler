using HowlerExamples.Services;
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
            var result = $"{_normalService.GetData()}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }     
        
        [HttpGet]
        public IActionResult GetDataHowler()
        {
            var result = _serviceUsingHowler.GetData() + "\n" + string.Join("\n", _logger.GetLogs());
            _logger.Clear();
            return Ok(result);
        }       

        [HttpGet]
        public IActionResult GetMoreDataHowler()
        {
            var result = _serviceUsingHowler.GetMoreData() + "\n" + string.Join("\n", _logger.GetLogs());
            _logger.Clear();
            return Ok(result);
        }
    }
}