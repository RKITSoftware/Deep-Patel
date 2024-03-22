using ExceptionHandlingDemo.Business_Logic;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingDemo.Controllers
{
    /// <summary>
    /// Home Controller for demonstrating Exception using DeveloperExceptionPage and ExceptionHandler
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLHomeController : ControllerBase
    {
        /// <summary>
        /// Generate Exception from the Business Logic Class and Catch it.
        /// </summary>
        /// <returns></returns>
        [HttpGet("", Order = -10)]
        [ProducesResponseType(200)]
        public ActionResult Index()
        {
            BLHome.Get();
            return Ok();
        }

        /// <summary>
        /// ExceptionHandler URL that handles exception during when thr all API's are in live.
        /// </summary>
        /// <returns><see cref="HttpRequestException"/> with exception message</returns>
        [HttpGet("Error", Order = int.MaxValue - 1)]
        public ActionResult<HttpRequestException> Error()
        {
            return new HttpRequestException("An unhandled Exception occured during your request.");
        }
    }
}
