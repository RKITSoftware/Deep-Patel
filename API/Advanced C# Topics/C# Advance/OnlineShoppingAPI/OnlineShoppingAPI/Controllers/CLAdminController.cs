using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLAdmin")]
    [BasicAuth]
    [Authorize(Roles = "Admin")]
    public class CLAdminController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage CreateAdmin(ADM01 objAdmin)
        {
            return BLAdmin.Create(objAdmin);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public HttpResponseMessage DeleteAdmin(int id)
        {
            return BLAdmin.Delete(id);
        }

        [HttpPatch]
        [Route("Password/Change/{username}/{newPassword}")]
        public HttpResponseMessage ChangePassword(string username, string newPassword)
        {
            return BLAdmin.ChangePassword(username, newPassword);
        }
    }
}
