using OnlineShoppingAPI.Business_Logic;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    public class CLAuthenticationController : ApiController
    {
        private BLAuthentication _authentication;

        public CLAuthenticationController()
        {
            _authentication = new BLAuthentication();
        }

        [HttpGet]
        [Route("Login")]
        public HttpResponseMessage LogIn(string username, string password)
        {
            return _authentication.LogIn(username, password);
        }

        [HttpGet]
        [Route("Logout")]
        public HttpResponseMessage LogOut()
        {
            return _authentication.LogOut();
        }
    }
}
