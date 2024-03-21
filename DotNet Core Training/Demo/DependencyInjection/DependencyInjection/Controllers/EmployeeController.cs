using DependencyInjection.Interface;
using DependencyInjection.Model;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    /// <summary>
    /// Employee controller for handling employee related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        /// <summary>
        /// employeeServices for EmployeeController.
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initialize services of controller.
        /// </summary>
        /// <param name="employeeService">Employee services instance</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Returns the Employee list.
        /// </summary>
        /// <returns><see cref="List{T}"/></returns>
        [HttpGet("")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<EMP01>))]
        public ActionResult<IEnumerable<EMP01>> GetAll()
        {
            return Ok(_employeeService.GetAll());
        }

        /// <summary>
        /// Returns employee who match with the id given by request.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns><see cref="EMP01"/></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(EMP01))]
        [ProducesResponseType(404)]
        public ActionResult<EMP01> GetById(int id)
        {
            if (!_employeeService.Exists(id))
            {
                return NotFound();
            }

            return Ok(_employeeService.GetById(id));
        }

        /// <summary>
        /// Create a employee object and add it to list.
        /// </summary>
        /// <param name="objEmployee">Employee information</param>
        /// <returns>Created statuscode if employee created else BadRequest.</returns>
        [HttpPost("Add")]
        [ProducesResponseType(201, Type = typeof(string))]
        [ProducesResponseType(400)]
        public ActionResult Add(EMP01 objEmployee)
        {
            if (_employeeService.Create(objEmployee))
            {
                return StatusCode(201, "Employee Created Successfully.");
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete the employee from the list of employees.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Ok response if employee deleted else gives response to specific pre-conditions.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult Delete(int id)
        {
            if (!_employeeService.Exists(id))
            {
                return NotFound();
            }

            if (_employeeService.Delete(id))
            {
                return Ok("Employee Deleted Successfully.");
            }

            return BadRequest();
        }
    }
}
