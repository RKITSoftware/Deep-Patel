using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLSuplier")]
    //[BearerAuth]
    public class CLSUP01Controller : ApiController
    {
        private readonly ISUP01Service _sup01Service;

        public CLSUP01Controller()
        {
            _sup01Service = new BLSUP01();
        }

        [HttpPatch]
        [Route("Change/Email")]
        //[Authorize(Roles = "Suplier,Admin")]
        public IHttpActionResult ChangeEmail(string username,
            string password, string newEmail)
        {
            Response response;
            _sup01Service.ChangeEmail(username, password, newEmail, out response);

            return Ok(response);
        }

        [HttpPatch]
        [Route("Change/Password")]
        //[Authorize(Roles = "Suplier,Admin")]
        public IHttpActionResult ChangePassword(string username,
            string oldPassword, string newPassword)
        {
            Response response;
            _sup01Service.ChangePassword(username, oldPassword, newPassword, out response);

            return Ok(response);
        }

        [HttpPost]
        [Route("Create")]
        //[Authorize(Roles = "Admin")]
        //[ValidateModel]
        public IHttpActionResult CreateSuplier(DTOSUP01 objSUP01DTO)
        {
            Response response;
            _sup01Service.PreSave(objSUP01DTO, EnmOperation.Create);

            if (_sup01Service.Validation(out response))
            {
                _sup01Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteSuplier/{id}")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteSuplier(int id)
        {
            Response response;
            _sup01Service.Delete(id, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetSupliers")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetSupliers()
        {
            Response response;
            _sup01Service.GetAll(out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetSuplier/{id}")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetSuplierById(int id)
        {
            Response response;
            _sup01Service.GetById(id, out response);

            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateSuplier")]
        //[Authorize(Roles = "Admin,Suplier")]
        //[ValidateModel]
        public IHttpActionResult UpdateCustomer(DTOSUP01 objDTOSUP01)
        {
            Response response;
            _sup01Service.PreSave(objDTOSUP01, EnmOperation.Update);

            if (_sup01Service.Validation(out response))
            {
                _sup01Service.Save(out response);
            }

            return Ok(response);
        }
    }
}
