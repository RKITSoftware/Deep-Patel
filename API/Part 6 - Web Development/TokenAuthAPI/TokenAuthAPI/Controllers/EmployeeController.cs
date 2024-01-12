using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Controllers
{
    /// <summary>
    /// Controller for handling employee-related operations with token-based authentication
    /// </summary>
    public class EmployeeController : ApiController
    {
        #region Public Properties

        /// <summary>
        /// Static list to store employee data
        /// </summary>
        public static List<Employee> empList;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor to initialize the employee list with sample data
        /// </summary>
        public EmployeeController()
        {
            empList = Employee.GetEmployees();
        }

        #endregion

        #region API Endpoints

        /// <summary>
        /// Action method to get employee by ID, accessible by users with the "User" role
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        [Authorize(Roles = ("User"))]
        public HttpResponseMessage GetEmployeesById(int id)
        {
            // Find the employee with the specified ID in the list
            var employee = empList.FirstOrDefault(e => e.Id == id);

            // Return an HTTP response with the employee information
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        /// <summary>
        /// Action method to get some employees, accessible by users with the "Admin" or "SuperAdmin" roles
        /// </summary>
        /// <returns>Few Employee Data</returns>
        [Authorize(Roles = ("Admin, SuperAdmin"))]
        [Route("api/Employee/GetSomeEmployee")]
        public HttpResponseMessage GetSomeEmployees()
        {
            // Filter employees with ID less than 4 and convert to a list
            var employee = empList.Where(e => e.Id < 4).ToList();

            // Return an HTTP response with the selected employees
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        /// <summary>
        /// Action method to get all employees, accessible by users with the "SuperAdmin" role
        /// </summary>
        /// <returns>All Employee Data</returns>
        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Employee/GetAllEmployee")]
        public HttpResponseMessage GetEmployees()
        {
            // Convert all employees to a list
            var employee = empList.ToList();

            // Return an HTTP response with all employees
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }

        #endregion
    }
}
