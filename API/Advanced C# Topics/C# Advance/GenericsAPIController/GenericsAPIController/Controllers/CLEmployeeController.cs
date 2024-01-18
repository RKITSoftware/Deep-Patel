using GenericsAPIController.Interface;
using GenericsAPIController.Models;
using System.Web.Http;

namespace GenericsAPIController.Controllers
{
    public class CLEmployeeController : ApiController
    {
        private IGenericService<EMP01> _employeeService;
        private CLGenericController<EMP01> _genericEmployeeController;

        public CLEmployeeController(IGenericService<EMP01> emplooyeeService)
        {
            _employeeService = emplooyeeService;

            if (_employeeService != null)
            {
                _genericEmployeeController = new CLGenericController<EMP01>(_employeeService);
            }
        }
    }
}
