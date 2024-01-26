using Microsoft.Web.Http;
using System.Web.Http;

namespace DemoVersioning.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Values")]
    public class ValuesV1Controller : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok("1");
        }
    }
}
