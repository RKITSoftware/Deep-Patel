using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Models;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling admin-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CLAdminController : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IAdminService"/>.
        /// </summary>
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
            _adminService.PreSave(objAdminDto);

            if (!_adminService.Validation())
                return BadRequest();

            return Ok(_adminService.CreateAdmin());
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

        /// <summary>
        /// Retrieves all instances of ADM01.
        /// </summary>
        /// <remarks>
        /// This method is used to handle HTTP GET requests without any specific route parameters.
        /// </remarks>
        /// <returns>An ActionResult containing IEnumerable collection of ADM01 instances.</returns>
        [HttpGet("")]
        public ActionResult<IEnumerable<ADM01>> GetAll()
        {
            return Ok(_adminService.GetAll());
        }
    }
}
