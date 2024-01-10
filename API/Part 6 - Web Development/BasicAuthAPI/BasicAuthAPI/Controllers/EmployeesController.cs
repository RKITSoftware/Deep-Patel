using BasicAuthAPI.BasicAuth;
using BasicAuthAPI.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace BasicAuthAPI.Controllers
{
    /// <summary>
    /// EmployeesController is a Web API controller for managing employee data.
    /// </summary>
    [BasicAuthenticationAttribute] // Apply basic authentication to the entire controller.
    public class EmployeesController : ApiController
    {
        #region Public Methods

        /// <summary>
        /// GetEmployees method returns a list of employees.
        /// </summary>
        /// <returns>List of employee</returns>
        public List<Employee> GetEmployees()
        {
            // Call the GetEmployees method from the Employee class to retrieve the list of employees.
            return Employee.GetEmployees();
        }

        #endregion
    }
}
