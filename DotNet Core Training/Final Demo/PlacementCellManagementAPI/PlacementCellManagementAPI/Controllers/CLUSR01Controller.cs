using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling user-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLUSR01Controller : ControllerBase
    {
        /// <summary>
        /// Endpoint to retrieve user data.
        /// </summary>
        /// <returns>Returns an HTTP response indicating success.</returns>
        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok(); // Returns 200 OK status
        }
    }
}
