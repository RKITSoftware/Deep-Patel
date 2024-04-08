using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLProduct")]
    //[CookieBasedAuth]
    public class CLPRO02Controller : ApiController
    {
        private readonly IPRO02Service _pro02Service;

        public CLPRO02Controller()
        {
            _pro02Service = new BLPRO02();
        }

        [HttpPost]
        [Route("Add")]
        //[ValidateModel]
        public IHttpActionResult Add(DTOPRO02 objPRO02DTO)
        {
            Response response;
            _pro02Service.PreSave(objPRO02DTO, EnmOperation.Create);

            if (_pro02Service.Validation(out response))
            {
                _pro02Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteProductV2")]
        public IHttpActionResult DeleteProductV2(int id)
        {
            Response response;
            _pro02Service.Delete(id, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetProductV2")]
        public IHttpActionResult GetPRO02()
        {
            Response response;
            _pro02Service.GetAll(out response);

            return Ok(response);
        }

        [HttpPatch]
        [Route("UpdateSellPrice")]
        public IHttpActionResult UpdateSellPrice(int id, int sellPrice)
        {
            Response response;
            _pro02Service.UpdateSellPrice(id, sellPrice, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllProductsInfo")]
        public IHttpActionResult GetAllProductInfo()
        {
            Response response;
            _pro02Service.GetInformation(out response);

            return Ok(response);
        }
    }
}
