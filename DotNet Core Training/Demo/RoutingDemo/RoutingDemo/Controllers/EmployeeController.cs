using Microsoft.AspNetCore.Mvc;
using RoutingDemo.Business_Logic;
using RoutingDemo.Model;

namespace RoutingDemo.Controllers
{
    /// <summary>
    /// Controller for managing employee-related API endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private BLEmployee _blEmployee;

        /// <summary>
        /// Initializes a new instance of the EmployeeController class.
        /// </summary>
        public EmployeeController()
        {
            _blEmployee = new BLEmployee();
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>An action result containing the list of employees.</returns>
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(_blEmployee.Get());
        }

        /// <summary>
        /// Retrieves an employee by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>An action result containing the employee if found; otherwise, NotFoundResult.</returns>
        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_blEmployee.Get(id));
        }

        /// <summary>
        /// Retrieves employees by company name.
        /// </summary>
        /// <param name="companyName">The name of the company to filter employees by.</param>
        /// <returns>An action result containing the list of employees belonging to the specified company.</returns>
        [HttpGet("GetByCompany")]
        public IActionResult Get(string companyName)
        {
            return Ok(_blEmployee.GetByCompany(companyName));
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="objEmployee">The employee object to add.</param>
        /// <returns>A status code indicating the result of the operation.</returns>
        [HttpPost("Add")]
        public IActionResult CreateEmployee(EMP01 objEmployee)
        {
            if (_blEmployee.Add(objEmployee))
                return StatusCode(201, "Employee Created Successfully.");

            return BadRequest();
        }
    }
}
