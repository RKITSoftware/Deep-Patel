using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/cart")]
    //[BearerAuth]
    public class CLCRT01Controller : ApiController
    {
        private readonly ICRT01Service _crt01Service;

        public CLCRT01Controller()
        {
            _crt01Service = new BLCRT01();
        }

        [HttpPost]
        [Route("AddItem")]
        public IHttpActionResult AddItemToCart(DTOCRT01 objDTOCRT01)
        {
            _crt01Service.PreSave(objDTOCRT01, EnmOperation.Create);

            if (_crt01Service.Validation(out Response response))
            {
                _crt01Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetCustomerCartData(int id)
        {
            _crt01Service.GetCUS01CRT01Details(id, out Response response);
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IHttpActionResult DeleteItemFromCart(int id)
        {
            Response response;
            _crt01Service.Delete(id, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("GenerateOtp")]
        public IHttpActionResult GenerateOtpForBuying(int id)
        {
            _crt01Service.Generate(id, out Response response);
            return Ok(response);
        }

        [HttpPost]
        [Route("VerifyOTP/{id}")]
        public IHttpActionResult VerifyAndBuyItems(int id, string otp)
        {
            _crt01Service.VerifyAndBuy(id, otp, out Response response);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetCartInfo/{id}")]
        public IHttpActionResult GetCartInfo(int id)
        {
            _crt01Service.GetFullCRT01InfoOfCUS01(id, out Response response);
            return Ok(response);
        }

        [HttpGet]
        [Route("BuyItem/{id}")]
        public IHttpActionResult BuyItem(int id)
        {
            _crt01Service.BuySingleItem(id, out Response response);
            return Ok(response);
        }
    }
}
