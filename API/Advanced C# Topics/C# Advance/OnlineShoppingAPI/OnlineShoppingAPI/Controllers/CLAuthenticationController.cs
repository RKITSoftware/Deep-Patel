using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller class handling authentication-related HTTP requests.
    /// </summary>
    public class CLAuthenticationController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling authentication endpoints.
        /// </summary>
        private BLAuthentication _authentication;

        /// <summary>
        /// Business logic class instance for generating jwt token
        /// </summary>
        private BLToken _blToken;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLAuthenticationController()
        {
            _authentication = new BLAuthentication();
            _blToken = new BLToken();
        }

        /// <summary>
        /// Handles HTTP GET request for user login.
        /// </summary>
        /// <param name="username">The username for login.</param>
        /// <param name="password">The password for login.</param>
        /// <returns>
        /// HTTP response message indicating the success or failure of the login attempt.
        /// </returns>
        [HttpGet]
        [Route("Login")]
        public HttpResponseMessage LogIn(string username, string password)
        {
            return _authentication.LogIn(username, password);
        }

        /// <summary>
        /// Handles HTTP GET request for user logout.
        /// </summary>
        /// <returns>
        /// HTTP response message indicating the success or failure of the logout attempt.
        /// </returns>
        [HttpGet]
        [Route("Logout")]
        public HttpResponseMessage LogOut()
        {
            return _authentication.LogOut();
        }

        /// <summary>
        /// Handles HTTP GET request for generating jwt token.
        /// </summary>
        /// <param name="username">The username for generating token.</param>
        /// <param name="password">The password for generating token.</param>
        /// <returns>
        /// HTTP response message indicating the success or failure of generating token process.
        /// </returns>
        [HttpGet]
        [Route("Generate")]
        public HttpResponseMessage GenerateToken(string username, string password)
        {
            USR01 objUser = BLHelper.GetUser(username, password);

            if (objUser == null)
                return BLHelper.ResponseMessage(HttpStatusCode.NotFound, "Credentials are invalid.");

            return _blToken.GenerateToken(Guid.NewGuid(), objUser);
        }

        /// <summary>
        /// Handles HTTP GET request for user logout.
        /// </summary>
        /// <returns>
        /// HTTP response message indicating the success or failure of the logout attempt.
        /// </returns>
        [HttpGet]
        [Route("JWTTokenLogout")]
        public HttpResponseMessage JwtTokenLogOut(string username)
        {
            return _blToken.LogOut(username);
        }
    }
}
