using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using EmployeeAPI.Filter;
using System.Web;

namespace EmployeeAPI.Controllers
{
    /// <summary>
    /// EmployeeController class responsible for managing employee data via Web API.
    /// </summary>
    public class EmployeeController : ApiController
    {
        #region Public Properties

        /// <summary>
        /// Static list to store employee data.
        /// </summary>
        public static List<Employee> empList;

        #endregion

        #region Constructor

        /// <summary>
        /// Static constructor initializes the empList with sample employee data.
        /// </summary>
        static EmployeeController()
        {
            empList = new List<Employee>
            {
                new Employee { ID = 1, Name = "Deep", Salary = 50000, Designation = "Full Stack Developer"},
                new Employee { ID = 2, Name = "Vishal", Salary = 50000, Designation = "Full Stack Developer"},
                new Employee { ID = 3, Name = "Prajval", Salary = 50000, Designation = "Full Stack Developer"},
            };
        }

        #endregion

        #region API Endpoints

        /// <summary>
        /// HTTP GET endpoint to retrieve all employee data.
        /// </summary>
        /// <returns>List of employee data</returns>
        [HttpGet]
        [Route("api/get/alldata")]
        public IHttpActionResult GetData()
        { 
            return Ok(empList);
        }

        /// <summary>
        /// This action is decorated with HttpGet attribute, indicating it should handle HTTP GET requests.
        /// The route specifies the endpoint for this action: "/api/get/data"
        /// </summary>
        /// <param name="id">For Searching in Employee</param>
        /// <returns>Employee Detais</returns>
        /// <exception cref="NotImplementedException">Method is not implement</exception>
        [HttpGet]
        [Route("api/get/data")]
        // This custom exception filter is applied to handle the NotImplementedException and return a specific response.
        [NotImplExceptionFilter]
        public IHttpActionResult GetData(int id)
        {
            // Throw a NotImplementedException with a message indicating that this method is not implemented.
            throw new NotImplementedException("This method is not implemented");
        }

        /// <summary>
        /// HTTP POST endpoint to add a new employee to the empList.
        /// </summary>
        /// <param name="id">Employee id for searching</param>
        /// <param name="employee">Employee Data</param>
        /// <returns>List of employee data</returns>
        [HttpPost]
        [Route("api/post/employee")]
        public IHttpActionResult PostData(int id, Employee employee)
        {
            // Create a new Employee instance using the provided data.
            Employee emp = new Employee
            {
                ID = id,
                Name = employee.Name,
                Salary = employee.Salary,
                Designation = employee.Designation
            };

            // Add the new employee to the empList.
            empList.Add(emp);

            // Return the updated empList in the response.
            return Ok(empList);
        }

        /// <summary>
        /// HTTP PUT endpoint to update an existing employee's information.
        /// </summary>
        /// <param name="id">Employee id for searching</param>
        /// <param name="employee">Employee Data</param>
        /// <returns>List of employee data</returns>
        [HttpPut]
        [Route("api/put/employee")]
        public IHttpActionResult PutData(int id, Employee employee)
        {
            // Find the employee in the empList based on the provided ID.
            Employee emp = empList.Find(e => e.ID == id);

            // Update the employee's information with the values from the request.
            emp.ID = employee.ID;
            emp.Name = employee.Name;
            emp.Salary = employee.Salary;
            emp.Designation = employee.Designation;

            // Return the updated empList in the response.
            return Ok(empList);
        }

        /// <summary>
        /// HTTP DELETE endpoint to remove an employee based on the provided ID.
        /// </summary>
        /// <param name="id">Employee id for delete</param>
        /// <returns>List of employee data</returns>
        [HttpDelete]
        [Route("api/delete/employee")]
        public IHttpActionResult DeleteData(int id)
        {
            // Find and remove the employee from empList based on the provided ID.
            empList.Remove(empList.Find(e => e.ID == id));

            // Return the updated empList in the response.
            return Ok(empList);
        }

        #endregion
    }
}
