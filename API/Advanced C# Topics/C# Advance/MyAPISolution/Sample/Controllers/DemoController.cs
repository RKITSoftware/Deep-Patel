using MyLibrary.Filter;
using System.Web.Http;

namespace Sample.Controllers
{
    public class DemoController : ApiController
    {
        [CacheFilter(TimeDuration = 10)]
        public IHttpActionResult Get()
        {
            int a = 1;
            int b = 10 / a;

            return Ok(b);
        }

        public IHttpActionResult Post()
        {
            return Ok("Done");
        }
    }
}
