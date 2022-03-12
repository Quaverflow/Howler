using Howler;
using HowlerExamples.CrossCuttingConcerns;
using HowlerExamples.Helpers;
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
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }     
        
        [HttpGet]
        public IActionResult GetDataHowler()
        {
            var data = _howler.Invoke(()=> _serviceUsingHowler.GetData(), StructuresIds.GetStructureId);
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }       

        [HttpGet]
        public IActionResult GetMoreDataHowler()
        {
            var data = _howler.Invoke(()=> _serviceUsingHowler.GetMoreData(), StructuresIds.GetStructureId);
            var result = $"{data}\n{string.Join("\n", _logger.GetLogs())}";
            _logger.Clear();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult PostDataHowler([FromBody] Dto dto)
        {
           _howler.Invoke(()=> _serviceUsingHowler.PostData(dto), StructuresIds.PostStructureId, dto);
            var result = string.Join("\n", _logger.GetLogs());
            _logger.Clear();
            return Ok(result);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult PostDataHowlerGeneric([FromBody] DtoGeneric dto)
        {
           _howler.Invoke(()=> _serviceUsingHowler.PostDataGenerics(dto), StructuresIds.PostStructureId, dto);
            var result = string.Join("\n", _logger.GetLogs());
            _logger.Clear();
            return Ok(result);
        }
    }
}