using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling admin-related operations.
    /// </summary>
    [RoutePrefix("api/CLAdmin")]
    //[BearerAuth] // Bearer token authentication is required
    //[Authorize(Roles = "Admin")] //  Authorization based on roles is required
    public class CLADM01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="IADM01Service"/>
        /// </summary>
        private readonly IADM01Service _adm01Service;

        /// <summary>
        /// Initializes a new instance of the CLADM01Controller class.
        /// </summary>
        public CLADM01Controller()
        {
            // Initialize the service used for admin operations
            _adm01Service = new BLADM01();
        }

        /// <summary>
        /// Creates a new admin.
        /// </summary>
        /// <param name="objDTOADM01">DTO containing admin data.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel] // Applies model validation
        public IHttpActionResult CreateAdmin(DTOADM01 objDTOADM01)
        {
            Response response;

            // Prepare admin data for saving
            _adm01Service.PreSave(objDTOADM01, EnmOperation.Create);

            // Validate admin data
            if (_adm01Service.Validation(out response))
            {
                // Save admin data
                _adm01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Changes admin email.
        /// </summary>
        /// <param name="username">Admin username.</param>
        /// <param name="password">Admin password.</param>
        /// <param name="newEmail">New email address.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPatch]
        [Route("Change/Email")]
        public IHttpActionResult ChangeEmail(string username,
            string password, string newEmail)
        {
            Response response;
            _adm01Service.ChangeEmail(username, password, newEmail, out response);

            return Ok(response);
        }

        /// <summary>
        /// Changes admin password.
        /// </summary>
        /// <param name="username">Admin username.</param>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="newPassword">New password.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPatch]
        [Route("Change/Password")]
        public IHttpActionResult ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            Response response;
            _adm01Service.ChangePassword(username, oldPassword, newPassword, out response);

            return Ok(response);
        }

        /// <summary>
        /// Deletes an admin by ID.
        /// </summary>
        /// <param name="id">Admin ID.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteAdmin(int id)
        {
            Response response;
            _adm01Service.Delete(id, out response);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves profit data for a specific date.
        /// </summary>
        /// <param name="date">Date for which profit data is requested.</param>
        /// <returns>Response containing profit data for the specified date.</returns>
        [HttpGet]
        [Route("ShowProfit")]
        public IHttpActionResult GetProfit(string date)
        {
            Response response;
            _adm01Service.GetProfit(date, out response);

            return Ok(response);
        }
    }
}
