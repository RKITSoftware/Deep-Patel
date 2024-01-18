using GenericsAPIController.Interface;
using System.Web.Http;

namespace GenericsAPIController.Controllers
{
    public class CLGenericController<T> : ApiController 
        where T : class
    {
        public IGenericService<T> _genericService;

        public CLGenericController(IGenericService<T> genericService)
        {
            _genericService = genericService;
        }
    }
}
