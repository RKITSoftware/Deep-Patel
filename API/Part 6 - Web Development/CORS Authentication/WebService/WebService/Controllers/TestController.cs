using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    // Controller for handling test operations with CORS enabled
    [EnableCors(origins: "https://localhost:44315,https://www.google.com", headers: "*", methods: "*")]
    public class TestController : ApiController
    {
        #region TestController Endpoints

        /// <summary>
        /// Action method for handling HTTP GET requests
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            // Return a response with a message for HTTP GET requests
            return new HttpResponseMessage()
            {
                Content = new StringContent("GET: Test message")
            };
        }

        /// <summary>
        /// Action method for handling HTTP POST requests
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Post()
        {
            // Return a response with a message for HTTP POST requests
            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message")
            };
        }

        /// <summary>
        /// Action method for handling HTTP PUT requests
        /// </summary>
        /// <returns></returns>
        [DisableCors]
        public HttpResponseMessage Put()
        {
            // Return a response with a message for HTTP PUT requests
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }

        #endregion
    }
}
