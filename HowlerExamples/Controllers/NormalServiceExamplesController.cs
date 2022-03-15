using ExamplesCore.Helpers;
using ExamplesCore.Models;
using ExamplesCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NormalServiceExamplesController : ControllerBase
    {
        private readonly INormalService _normalService;

        public NormalServiceExamplesController(INormalService normalService)
        {
            _normalService = normalService;
        }

        [HttpGet]
        public IActionResult GetDataNormal()
        {
            Cleanup();
            var data = _normalService.GetData();
            var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostDataHowlerAndNotify([FromBody] DtoNotifiable dto)
        {
            Cleanup();
            var savedEntity = await _normalService.PostDataAndNotify(dto);

            var result = string.Join("\n", FakesRepository.Logs);
            result += "\n" + string.Join("\n", FakesRepository.EmailsSent);
            result +=" \n" +  string.Join("\n", FakesRepository.SmsSent);
            result += $"\nsaved entity: {savedEntity}";
     

            return Ok(result);
        }

        private void Cleanup()
        {
            FakesRepository.Logs.Clear();
            FakesRepository.EmailsSent.Clear();
            FakesRepository.SmsSent.Clear();
        }
    }
}