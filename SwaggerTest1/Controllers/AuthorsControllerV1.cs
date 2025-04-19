
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    /// <summary>
    /// sdfgsdfgsdfgsdfgsdfgs
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]    
    public class AuthorsV1Controller : ControllerBase
    {
        /// <summary>
        /// sdfgsdfgsdf
        /// </summary>
        public AuthorsV1Controller()
        { }

        /// <summary>
        /// sdfgsdfgsd
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<string>>> func1()
        {
            return Enumerable.Range(1, 10).Select(index => "Authors" + index).ToArray();
        }

        /// <summary>
        /// سیبلسیبلایسبایبایبل
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpPost("{authorId}")]
        [ApiVersion("2.0")]
        public ActionResult<string> func2(int authorId)
        {
            if (authorId < 1 || authorId > 10)
            {
                return NotFound();
            }

            return Ok("Authors" + authorId);
        }

        /// <summary>
        /// esdfgsdfgsdfgsdf
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("saeed")]
        public Task<ActionResult<string>> Func3()
        {
            return Task.FromResult<ActionResult<string>>(Ok("Authors" + 1));
        }
    }
}

