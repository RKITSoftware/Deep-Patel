using BogusAPI.Business_Logic;
using System.Web.Http;

namespace BogusAPI.Controllers
{
    public class CLStudentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetFakeData()
        {
            return Ok(BLStudent.FakeData());
        }
    }
}
