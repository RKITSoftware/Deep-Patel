using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling admin-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous] // Allow anonymous access to these endpoints
    public class CLAdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        /// <summary>
        /// Initializes a new instance of the CLAdminController class.
        /// </summary>
        /// <param name="adminService">The admin service instance to handle admin-related operations.</param>
        public CLAdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Endpoint to create a new admin.
        /// </summary>
        /// <param name="objAdminDto">DTO containing admin information.</param>
        /// <returns>Returns HTTP status code indicating the result of the operation.</returns>
        [HttpPost("")]
        public ActionResult CreateAdmin(DtoADM01 objAdminDto)
        {
            if (_adminService.CreateAdmin(objAdminDto))
                return StatusCode(201, "Admin Created Successfully.");

            return BadRequest("Admin can't be created.");
        }

        /// <summary>
        /// Endpoint to delete an admin by ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>Returns HTTP status code indicating the result of the operation.</returns>
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteAdmin(int id)
        {
            if (_adminService.DeleteAdmin(id))
                return Ok("Admin deleted successfully.");

            return BadRequest("Admin can't be deleted");
        }
    }
}
