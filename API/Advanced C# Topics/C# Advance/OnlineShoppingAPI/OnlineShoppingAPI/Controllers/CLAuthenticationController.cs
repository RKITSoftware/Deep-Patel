using OnlineShoppingAPI.Business_Logic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller class handling authentication-related HTTP requests.
    /// </summary>
    public class CLAuthenticationController : ApiController
    {
        // Business Logic instance for handling authentication operations.
        private BLAuthentication _authentication;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLAuthenticationController()
        {
            _authentication = new BLAuthentication();
        }

        /// <summary>
        /// Handles HTTP GET request for user login.
        /// </summary>
        /// <param name="username">The username for login.</param>
        /// <param name="password">The password for login.</param>
        /// <returns>HTTP response message indicating the success or failure of the login attempt.</returns>
        [HttpGet]
        [Route("Login")]
        public HttpResponseMessage LogIn(string username, string password)
        {
            return _authentication.LogIn(username, password);
        }

        /// <summary>
        /// Handles HTTP GET request for user logout.
        /// </summary>
        /// <returns>HTTP response message indicating the success or failure of the logout attempt.</returns>
        [HttpGet]
        [Route("Logout")]
        public HttpResponseMessage LogOut()
        {
            return _authentication.LogOut();
        }
    }
}
