using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Controllers.Attribute;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="ADM01"/> related api's
    /// </summary>
    [RoutePrefix("api/CLADM01")]
    [Authorize(Roles = "Admin")]
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
            _adm01Service = new BLADM01Handler();
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
            Response response = _adm01Service.ChangeEmailValidation(username, password, newEmail);

            if (!response.IsError)
            {
                response = _adm01Service.ChangeEmail(username, newEmail);
            }

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
            Response response = _adm01Service.ChangePassword(username, oldPassword, newPassword);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new admin.
        /// </summary>
        /// <param name="objDTOADM01">DTO containing admin data.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public IHttpActionResult CreateAdmin(DTOADM01 objDTOADM01)
        {
            Response response;
            _adm01Service.Operation = EnmOperation.A;

            _adm01Service.PreSave(objDTOADM01);
            response = _adm01Service.Validation();

            if (!response.IsError)
                response = _adm01Service.Save();

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
            Response response = _adm01Service.DeleteValidation(id);

            if (!response.IsError)
            {
                response = _adm01Service.Delete(id);
            }

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
            return Ok(_adm01Service.GetProfit(date));
        }
    }
}
