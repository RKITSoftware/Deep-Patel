using Microsoft.Web.Http;
using System.Web.Http;

namespace DemoVersioning.Controllers
{
    [ApiVersion("2.0")]
    [RoutePrefix("api/v{version:apiVersion}/Values")]
    public class ValuesV2Controller : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("2");
        }
    }
}
