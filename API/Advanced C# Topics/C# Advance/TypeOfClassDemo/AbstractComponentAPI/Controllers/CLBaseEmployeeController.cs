using System.Web.Http;

namespace AbstractComponentAPI.Controllers
{
    /// <summary>
    /// Absract Controller for Versioning
    /// </summary>
    public abstract class CLBaseEmployeeController : ApiController
    {
        public abstract IHttpActionResult Get();
        public abstract IHttpActionResult Get(int id);
    }
}
