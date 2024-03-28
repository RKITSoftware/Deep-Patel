using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for student-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLStudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLStudentController"/> class.
        /// </summary>
        /// <param name="studentService">The student service.</param>
        public CLStudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="objStudentDto">The DTO representing the student to create.</param>
        /// <returns>Returns 201 if the student was created successfully, otherwise returns BadRequest.</returns>
        [HttpPost("")]
        public ActionResult Create(DtoSTU01 objStudentDto)
        {
            if (_studentService.Add(objStudentDto))
                return StatusCode(201, "Student created successfully.");

            return BadRequest(ModelState);
        }
    }
}
