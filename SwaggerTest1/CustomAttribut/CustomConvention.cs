using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SwaggerTest1
{
    public static class CustomConventions
    {
        // this is apply for insert function - name must be insert
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Any)]
        public  static void Insert(object modal)
        { 
        
        }
    }
}
