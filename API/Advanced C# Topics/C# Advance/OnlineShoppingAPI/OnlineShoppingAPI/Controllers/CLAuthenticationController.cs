using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Net;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller class handling authentication-related HTTP requests.
    /// </summary>
    [RoutePrefix("api/CLAuthentication")]
    [AllowAnonymous]
    public class CLAuthenticationController : ApiController
    {
        /// <summary>
        /// USR01 model services.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        /// <summary>
        /// Business logic class instance for generating jwt token and validating token.
        /// </summary>
        private readonly BLToken _blToken;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLAuthenticationController()
        {
            _blToken = new BLToken();
            _usr01Service = new BLUSR01Handler();
        }

        /// <summary>
        /// Handles HTTP GET request for generating jwt token.
        /// </summary>
        /// <param name="username">The username for generating token.</param>
        /// <param name="password">The password for generating token.</param>
        /// <returns>
        /// Response indicating the success or failure of generating token process.
        /// </returns>
        [HttpGet]
        [Route("Generate")]
        public IHttpActionResult GenerateToken(string username, string password)
        {
            Response response = new Response();

            USR01 objUser = _usr01Service.GetUser(username, password);

            if (objUser != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Data = _blToken.GenerateToken(Guid.NewGuid(), objUser);
            }
            else
            {
                response.IsError = true;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Credentials are invalid.";
            }

            return Ok(response);
        }
    }
}