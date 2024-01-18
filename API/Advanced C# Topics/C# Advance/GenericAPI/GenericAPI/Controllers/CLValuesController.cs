using GenericAPI.Interface;
using System.Web.Http;

namespace GenericAPI.Controllers
{
    public class CLValuesController : ApiController
    {
        private readonly IRepository _repository;

        public CLValuesController() { }

        public CLValuesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_repository.GetData("Deep"));
        }
    }
}
