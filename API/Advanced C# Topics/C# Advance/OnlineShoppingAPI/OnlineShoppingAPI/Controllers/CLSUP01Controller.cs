using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller to handle <see cref="SUP01"/> related api endpoints.
    /// </summary>
    [RoutePrefix("api/CLSUP01")]
    public class CLSUP01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="ISUP01Service"/>.
        /// </summary>
        private readonly ISUP01Service _sup01Service;

        /// <summary>
        /// Constructor to initialize the <see cref="CLSUP01Controller"/>.
        /// </summary>
        public CLSUP01Controller()
        {
            _sup01Service = new BLSUP01();
        }

        /// <summary>
        /// Change the email of supplier.
        /// </summary>
        /// <param name="username">Username of supplier.</param>
        /// <param name="password">Password of supplier.</param>
        /// <param name="newEmail">New email address of supplier.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPatch]
        [Route("Change/Email")]
        [Authorize(Roles = "Suplier,Admin")]
        public IHttpActionResult ChangeEmail(string username,
            string password, string newEmail)
        {
            _sup01Service.ChangeEmail(username, password, newEmail, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Change the password of supplier.
        /// </summary>
        /// <param name="username">Username of the supplier.</param>
        /// <param name="oldPassword">Current password of the supplier.</param>
        /// <param name="newPassword">New password of the supplier.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPatch]
        [Route("Change/Password")]
        [Authorize(Roles = "Suplier,Admin")]
        public IHttpActionResult ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            _sup01Service.ChangePassword(username, oldPassword, newPassword, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new supplier.
        /// </summary>
        /// <param name="objSUP01DTO">DTO Containing the supplier information.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IHttpActionResult CreateSuplier(DTOSUP01 objSUP01DTO)
        {
            _sup01Service.Operation = EnmOperation.Create;
            if (_sup01Service.PreValidation(objSUP01DTO, out Response response))
            {
                _sup01Service.PreSave(objSUP01DTO);

                if (_sup01Service.Validation(out response))
                    _sup01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete the suplier.
        /// </summary>
        /// <param name="id">Suplier id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("DeleteSuplier/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteSuplier(int id)
        {
            _sup01Service.Delete(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Gets the all suplier information.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetSupliers")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetSupliers()
        {
            _sup01Service.GetAll(out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Gets the suplier specified by id.
        /// </summary>
        /// <param name="id">sSuplier id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetSuplier/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetSuplierById(int id)
        {
            _sup01Service.GetById(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Updates the suplier information.
        /// </summary>
        /// <param name="objDTOSUP01">DTO containing the updated information of supplier.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateSuplier")]
        [Authorize(Roles = "Admin,Suplier")]
        [ValidateModel]
        public IHttpActionResult UpdateCustomer(DTOSUP01 objDTOSUP01)
        {
            _sup01Service.Operation = EnmOperation.Update;
            if (_sup01Service.PreValidation(objDTOSUP01, out Response response))
            {
                _sup01Service.PreSave(objDTOSUP01);

                if (_sup01Service.Validation(out response))
                    _sup01Service.Save(out response);
            }

            return Ok(response);
        }
    }
}
