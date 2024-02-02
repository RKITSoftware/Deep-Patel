using BasicAuthAPI.BasicAuth;
using BasicAuthAPI.Business_Logic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAuthAPI.Controllers
{
    /// <summary>
    /// EmployeesController is a Web API controller for managing employee data.
    /// </summary>
    [RoutePrefix("api/Employees")]
    [BasicAuthentication] // Apply basic authentication to the entire controller.
    public class CLEmployeesController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// GetEmployees method returns a list of employees.
        /// GetFewEmployees
        /// </summary>
        /// <returns>List of employee</returns>
        [Route("GetFewEmployees")]
        [Authorize(Roles = "User")]
        public HttpResponseMessage GetFewEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetFewEmployee());
        }

        // GetMoreEmployees
        [Route("GetMoreEmployees")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetMoreEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetMoreEmployee());
        }

        // GetAllEmployees
        [Route("GetAllEmployees")]
        [Authorize(Roles = "SuperAdmin")]
        public HttpResponseMessage GetAllEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetAllEmployee());
        }

        #endregion
    }
}
