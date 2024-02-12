using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLSuplier")]
    [BasicAuth]
    public class CLSuplierController : ApiController
    {
        private BLSuplier _blSuplier;

        public CLSuplierController()
        {
            _blSuplier = new BLSuplier();
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/CreateSuplier
        /// </summary>
        /// <param name="objNewSuplier">Suplier data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSuplier")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateSuplier(SUP01 objNewSuplier)
        {
            return _blSuplier.Create(objNewSuplier);
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/GetSupliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSupliers")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetSupliers()
        {
            return Ok(_blSuplier.GetAll());
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/CreateSuplier/List
        /// </summary>
        /// <param name="lstNewSupliers">New suplier list</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateSuplier/List")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateSuplierFromList(List<SUP01> lstNewSupliers)
        {
            return _blSuplier.CreateFromList(lstNewSupliers);
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/DeleteSuplier/{id}
        /// </summary>
        /// <param name="id">Suplier id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteSuplier/{id}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage DeleteSuplier(int id)
        {
            return _blSuplier.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/UpdateSuplier
        /// </summary>
        /// <param name="objUpdatedSuplier">updated suplier data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSuplier")]
        [Authorize(Roles = "Admin,Suplier")]
        public HttpResponseMessage UpdateCustomer(SUP01 objUpdatedSuplier)
        {
            return _blSuplier.Update(objUpdatedSuplier);
        }

        /// <summary>
        /// Endpoint :- api/CLSuplier/ChangePassword
        /// </summary>
        /// <param name="username">User name of suplier</param>
        /// <param name="newPassword">New password of suplier</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("ChangePassword")]
        [Authorize(Roles = "Suplier")]
        public HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _blSuplier.ChangePassword(username, oldPassword, newPassword);
        }
    }
}
