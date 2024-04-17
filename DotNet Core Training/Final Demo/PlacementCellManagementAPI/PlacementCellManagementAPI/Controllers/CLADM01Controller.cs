using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Controllers.Filters;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling admin-related HTTP requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CLADM01Controller : ControllerBase
    {
        /// <summary>
        /// Instance of <see cref="IADM01Service"/>.
        /// </summary>
        private readonly IADM01Service _adm01Service;

        /// <summary>
        /// Initializes a new instance of the CLAdminController class.
        /// </summary>
        /// <param name="adminService">The admin service instance to handle admin-related operations.</param>
        public CLADM01Controller(IADM01Service adminService)
        {
            _adm01Service = adminService;
        }

        /// <summary>
        /// Endpoint to create a new admin.
        /// </summary>
        /// <param name="objDTOADM01">DTO containing admin information.</param>
        /// <returns>Returns HTTP status code indicating the result of the operation.</returns>
        [HttpPost("")]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public IActionResult CreateAdmin(DTOADM01 objDTOADM01)
        {
            _adm01Service.Operation = EnmOperation.A;
            Response response = _adm01Service.PreValidation(objDTOADM01);

            if (!response.IsError)
            {
                _adm01Service.PreSave(objDTOADM01);
                response = _adm01Service.Validation();

                if (!response.IsError)
                    response = _adm01Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to delete an admin by ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>Returns HTTP status code indicating the result of the operation.</returns>
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteAdmin(int id)
        {
            Response response = _adm01Service.DeleteValidation(id);

            if (!response.IsError)
                response = _adm01Service.Delete(id);

            return Ok(response);
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
            return Ok(_adm01Service.GetAll());
        }
    }
}
