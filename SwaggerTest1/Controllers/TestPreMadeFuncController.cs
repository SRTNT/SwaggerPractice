using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerTest1.Controllers
{
    // Add For Controller
    //[ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Route("[controller]")]
    public class TestPreMadeFuncController : ControllerBase
    {
        // Add For Action
        //[ApiConventionMethod(typeof(DefaultApiConventions),
        //                     nameof(DefaultApiConventions.Get))]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("insert/{data}")]
        [ApiConventionMethod(typeof(CustomConventions),
                             nameof(CustomConventions.Insert))]
        public IActionResult Insert(string data)
        {
            return Ok();
        }
    }
}
