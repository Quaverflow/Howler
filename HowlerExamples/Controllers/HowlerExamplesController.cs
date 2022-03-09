using Microsoft.AspNetCore.Mvc;

namespace HowlerExamples.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HowlerExamplesController : ControllerBase
    {
        [HttpPost]
        public IActionResult Normal()
        {
            return Ok();
        }     
        
        [HttpPost]
        public IActionResult Howler()
        {
            return Ok();
        }
    }
}