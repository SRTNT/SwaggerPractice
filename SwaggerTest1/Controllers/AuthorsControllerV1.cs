
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthorsV1Controller : ControllerBase
    {
        public AuthorsV1Controller()
        { }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<string>>> func1()
        {
            return Enumerable.Range(1, 10).Select(index => "Authors" + index).ToArray();
        }

        [HttpPost("{authorId}")]
        [ApiVersion("2.0")]
        public async Task<ActionResult<string>> func2(int authorId)
        {
            if (authorId < 1 || authorId > 10)
            {
                return NotFound();
            }

            return Ok("Authors" + authorId);
        }

        [HttpPost]
        [Route("saeed")]
        public async Task<ActionResult<string>> func3()
        {
            return Ok("Authors" + 1);
        }
    }
}

