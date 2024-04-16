using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for student-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLSTU01Controller : ControllerBase
    {
        /// <summary>
        /// <see cref="ISTU01Service"/> for Controller.
        /// </summary>
        private readonly ISTU01Service _stu01Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLSTU01Controller"/> class.
        /// </summary>
        /// <param name="studentService">The student service.</param>
        public CLSTU01Controller(ISTU01Service studentService)
        {
            _stu01Service = studentService;
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="objStudentDto">The DTO representing the student to create.</param>
        /// <returns>Returns 201 if the student was created successfully, otherwise returns BadRequest.</returns>
        [HttpPost("")]
        public IActionResult Create(DTOSTU01 objDTOSTU01)
        {
            _stu01Service.Operation = EnmOperation.A;
            Response response = _stu01Service.PreValidation(objDTOSTU01);

            if (!response.IsError)
            {
                _stu01Service.PreSave(objDTOSTU01);
                response = _stu01Service.Validation();

                if (!response.IsError)
                {
                    response = _stu01Service.Save();
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>Returns a response indicating the outcome of the deletion operation.</returns>
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Response response = _stu01Service.DeleteValidation(id);

            if (!response.IsError)
            {
                response = _stu01Service.Delete(id);
            }

            return Ok(response);
        }
    }
}
