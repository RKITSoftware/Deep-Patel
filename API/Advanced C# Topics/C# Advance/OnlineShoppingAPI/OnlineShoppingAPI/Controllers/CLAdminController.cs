using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Dto;
using OnlineShoppingAPI.Filter;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Admin controller for handling api endpoints of admin
    /// </summary>
    [RoutePrefix("api/CLAdmin")]
    //[BearerAuth]
    //[Authorize(Roles = "Admin")]
    public class CLAdminController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling admin endpoints.
        /// </summary>
        private BLAdmin _blAdmin;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLAdminController()
        {
            _blAdmin = new BLAdmin();
        }

        /// <summary>
        /// Changes the email for an admin.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="password">Admin old password</param>
        /// <param name="newEmail">Admin new password</param>
        /// <returns>Email change response message</returns>
        [HttpPatch]
        [Route("Change/Email")]
        public HttpResponseMessage ChangeEmail(string username,
            string password, string newEmail)
        {
            return _blAdmin.ChangeEmail(username, password, newEmail);
        }

        /// <summary>
        /// Changes the password for an admin.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="oldPassword">Admin old password</param>
        /// <param name="newPassword">Admin new password</param>
        /// <returns>Password change response message</returns>
        [HttpPatch]
        [Route("Change/Password")]
        public HttpResponseMessage ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            return _blAdmin.ChangePassword(username, oldPassword, newPassword);
        }

        /// <summary>
        /// Creates a new admin.
        /// </summary>
        /// <param name="objAdmin">New admin information to create</param>
        /// <returns>Create response message</returns>
        [HttpPost]
        [Route("Add")]
        [ValidateModel]
        public HttpResponseMessage CreateAdmin(DtoADM01 objAdmin)
        {
            return _blAdmin.Create(objAdmin);
        }

        /// <summary>
        /// Deletes an admin by ID.
        /// </summary>
        /// <param name="id">Admin ID to delete</param>
        /// <returns>Delete response message</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteAdmin(int id)
        {
            return _blAdmin.Delete(id);
        }

        /// <summary>
        /// Get the profit of specific date
        /// </summary>
        /// <param name="date">Date in dd-MM-yyyy format</param>
        /// <returns>Profit for specific that date</returns>
        [HttpGet]
        [Route("ShowProfit")]
        public IHttpActionResult GetProfit(string date)
        {
            return Ok(_blAdmin.GetProfit(date));
        }
    }
}
