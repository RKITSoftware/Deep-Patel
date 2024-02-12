using CustomJWTBearerTokenAPI.Business_Logic;
using CustomJWTBearerTokenAPI.Models;
using System.Web.Http;

namespace CustomJWTBearerTokenAPI.Controllers
{
    /// <summary>
    /// Authentication controller handled the jwt token generation of project.
    /// </summary>
    public class CLAuthenticationController : ApiController
    {
        /// <summary>
        /// Generates a JWT token for the given username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>HTTP response containing the generated JWT token in the data field.</returns>
        [HttpPost]
        [Route("token")]
        public IHttpActionResult GenerateJwtToken(string username, string password)
        {
            // Retrieve user information based on the provided username and password
            USR01 objUser = BLUser.GetUser(username, password);

            // If the user is not found, return a 404 Not Found response
            if (objUser == null)
                return NotFound();

            // Generate a JWT token using the user information and return it in the response
            return Ok(new { data = BLToken.GenerateToken(objUser) });
        }
    }
}
