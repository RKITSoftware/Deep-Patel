using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Admin controller for handling api endpoints of admin
    /// </summary>
    [RoutePrefix("api/CLAdmin")]
    [BasicAuth]
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
        /// Creates a new admin.
        /// </summary>
        /// <param name="objAdmin">New admin information to create</param>
        /// <returns>Create response message</returns>
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage CreateAdmin(ADM01 objAdmin)
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
        /// Changes the password for an admin.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="oldPassword">Admin old password</param>
        /// <param name="newPassword">Admin new password</param>
        /// <returns>Password change response message</returns>
        [HttpPatch]
        [Route("Password/Change")]
        public HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _blAdmin.ChangePassword(username, oldPassword, newPassword);
        }
    }
}
