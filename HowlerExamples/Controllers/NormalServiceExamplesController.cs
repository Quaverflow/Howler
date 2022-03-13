using ExamplesCore.Helpers;
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
            var data = _normalService.GetData();
            var result = $"{data}\n{string.Join("\n", FakesRepository.Logs)}";
            FakesRepository.Logs.Clear();
            return Ok(result);
        }
    }
}