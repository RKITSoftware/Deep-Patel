using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthAPI.Business_Logic;

namespace TokenAuthAPI.Controllers
{
    /// <summary>
    /// Controller for handling employee-related operations with token-based authentication
    /// </summary>
    public class CLEmployeeController : ApiController
    {
        #region API Endpoints

        /// <summary>
        /// Action method to get employee by ID, accessible by users with the "User" role
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        [Authorize(Roles = ("User"))]
        public HttpResponseMessage GetEmployeesById(int id)
        {
            // Return an HTTP response with the employee information
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetEmployeeById(id));
        }

        /// <summary>
        /// Action method to get some employees, accessible by users with the "Admin" or "SuperAdmin" roles
        /// </summary>
        /// <returns>Few Employee Data</returns>
        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/Employee/GetSomeEmployee")]
        public HttpResponseMessage GetSomeEmployees()
        {
            // Return an HTTP response with the selected employees
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetSomeEmployee());
        }

        /// <summary>
        /// Action method to get all employees, accessible by users with the "SuperAdmin" role
        /// </summary>
        /// <returns>All Employee Data</returns>
        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Employee/GetAllEmployee")]
        public HttpResponseMessage GetEmployees()
        {
            // Return an HTTP response with all employees
            return Request.CreateResponse(HttpStatusCode.OK, BLEmployee.GetAllEmployee());
        }

        #endregion
    }
}
