using CustomJWTBearerTokenAPI.Security;
using System.Web.Http;

namespace CustomJWTBearerTokenAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations with token-based authentication.
    /// </summary>
    [RoutePrefix("api/user")]
    [TokenAuthentication]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Gets the name for an authorized user with the "Admin" role.
        /// </summary>
        /// <returns>HTTP response indicating the authorization status for an Admin user.</returns>
        [HttpGet]
        [Route("admindata")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetName()
        {
            return Ok("Admin is authorized");
        }

        /// <summary>
        /// Gets user data for an authorized user with the "User" role.
        /// </summary>
        /// <returns>HTTP response indicating the authorization status for a User.</returns>
        [HttpGet]
        [Route("userdata")]
        [Authorize(Roles = "User")]
        public IHttpActionResult GetUserData()
        {
            return Ok("User is authorized");
        }
    }
}
