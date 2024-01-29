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
    [Authorize(Roles = "Admin")]
    public class CLAdminController : ApiController
    {
        /// <summary>
        /// POST :- api/CLAdmin/Add
        /// </summary>
        /// <param name="objAdmin">New admin information to create</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage CreateAdmin(ADM01 objAdmin)
        {
            return BLAdmin.Create(objAdmin);
        }

        /// <summary>
        /// DELETE :- api/CLAdmin/Delete/1
        /// </summary>
        /// <param name="id">Delete id for deleting admin</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteAdmin(int id)
        {
            return BLAdmin.Delete(id);
        }

        /// <summary>
        /// PATCH :- api/CLAdmin/Password/Change/{username}/{newPassword}
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="newPassword">Admin new password</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("Password/Change")]
        public HttpResponseMessage ChangePassword([FromUri] string username, [FromUri] string newPassword)
        {
            return BLAdmin.ChangePassword(username, newPassword);
        }
    }
}
