using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        public static List<Employee> empList;

        public EmployeeController()
        {
            empList = Employee.GetEmployees();
        }

        [Authorize(Roles = ("User"))]
        public HttpResponseMessage GetEmployeesById(int id)
        {
            var employee = empList.FirstOrDefault(e => e.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/Employee/GetSomeEmployee")]
        public HttpResponseMessage GetSomeEmployees()
        {
            var employee = empList.Where(e => e.Id < 4).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Employee/GetAllEmployee")]
        public HttpResponseMessage GetEmployees()
        {
            var employee = empList.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }
    }
}
