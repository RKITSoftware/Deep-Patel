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
        /// <summary>
        /// Instance of a <see cref="IStudentService"/> for getting student related services.
        /// </summary>
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
            _studentService.PreSave(objStudentDto);

            if (!_studentService.Validation())
                return BadRequest();

            return Ok(_studentService.Add());
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>Returns Ok if the student was deleted successfully, otherwise returns BadRequest.</returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(400)]
        public ActionResult Delete(int id)
        {
            if (_studentService.Delete(id))
                return Ok("Student successfully deleted.");

            return BadRequest(ModelState);
        }
    }
}
