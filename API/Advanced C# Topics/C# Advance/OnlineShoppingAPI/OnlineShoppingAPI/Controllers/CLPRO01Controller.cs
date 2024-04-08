using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [Route("api/CLPRO01")]
    public class CLPRO01Controller : ApiController
    {
        private readonly IPRO01Service _pro01Service;

        public CLPRO01Controller()
        {
            _pro01Service = new BLPRO01();
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult CreateProduct(DTOPRO01 objPRO01DTO)
        {
            Response response;
            _pro01Service.PreSave(objPRO01DTO, EnmOperation.Create);

            if (_pro01Service.Validation(out response))
            {
                _pro01Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update(DTOPRO01 objDTOPRO01)
        {
            Response response;
            _pro01Service.PreSave(objDTOPRO01, EnmOperation.Update);

            if (_pro01Service.Validation(out response))
            {
                _pro01Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            Response response;
            _pro01Service.Delete(id, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("All")]
        public IHttpActionResult GetAll()
        {
            Response response;
            _pro01Service.GetAll(out response);

            return Ok(response);
        }

        [HttpPatch]
        [Route("UpdateQuantity/{id}")]
        public IHttpActionResult UpdateQuantity(int id, int quantity)
        {
            Response response;
            _pro01Service.UpdateQuantity(id, quantity, out response);

            return Ok(response);
        }
    }
}
