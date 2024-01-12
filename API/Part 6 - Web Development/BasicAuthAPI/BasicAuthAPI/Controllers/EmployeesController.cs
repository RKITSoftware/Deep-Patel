using BasicAuthAPI.BasicAuth;
using BasicAuthAPI.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAuthAPI.Controllers
{
    /// <summary>
    /// EmployeesController is a Web API controller for managing employee data.
    /// </summary>
    [RoutePrefix("api/Employees")]
    [BasicAuthenticationAttribute] // Apply basic authentication to the entire controller.
    public class EmployeesController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// GetEmployees method returns a list of employees.
        /// GetFewEmployees
        /// </summary>
        /// <returns>List of employee</returns>
        [Route("GetFewEmployees")]
        [BasicAuthorizationAttribute(Roles = "User")]
        public HttpResponseMessage GetFewEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.GetEmployees().Where(e => e.Id < 3));
        }

        // GetMoreEmployees
        [Route("GetMoreEmployees")]
        [BasicAuthorizationAttribute(Roles = "Admin")]
        public HttpResponseMessage GetMoreEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.GetEmployees().Where(e => e.Id < 5));
        }

        // GetAllEmployees
        [Route("GetAllEmployees")]
        [BasicAuthorizationAttribute(Roles = "SuperAdmin")]
        public HttpResponseMessage GetAllEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, Employee.GetEmployees());
        }

        #endregion
    }
}
