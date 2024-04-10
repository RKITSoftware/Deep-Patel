using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="CRT01"/> related HTTP requests.
    /// </summary>
    [RoutePrefix("api/CLCRT01")]
    public class CLCRT01Controller : ApiController
    {
        /// <summary>
        /// Interface service of <see cref="ICRT01Service"/> for <see cref="CLCRT01Controller"/>.
        /// </summary>
        private readonly ICRT01Service _crt01Service;

        /// <summary>
        /// To Initialize instances of <see cref="CLCRT01Controller"/>.
        /// </summary>
        public CLCRT01Controller()
        {
            _crt01Service = new BLCRT01();
        }

        /// <summary>
        /// Add item to the customer cart.
        /// </summary>
        /// <param name="objDTOCRT01">DTO containing the details.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("AddItem")]
        public IHttpActionResult AddItemToCart(DTOCRT01 objDTOCRT01)
        {
            _crt01Service.Operation = EnmOperation.Create;
            _crt01Service.PreSave(objDTOCRT01);

            if (_crt01Service.Validation(out Response response))
            {
                _crt01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Retrieve the customer cart information using Orm Lite.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetCustomerCartData(int id)
        {
            _crt01Service.GetCUS01CRT01Details(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Deleted the item from the customer cart.
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("DeleteItem/{id}")]
        public IHttpActionResult DeleteItemFromCart(int id)
        {
            _crt01Service.Delete(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Generates the OTP for 2-Factor Buy Process.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GenerateOtp")]
        public IHttpActionResult GenerateOtpForBuying(int id)
        {
            _crt01Service.Generate(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Verify the OTP that sent over the mail and bought the items if OTP Verified.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="otp">OTP (One Time Password) sent over mail.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("VerifyOTP/{id}")]
        public IHttpActionResult VerifyAndBuyItems(int id, string otp)
        {
            _crt01Service.VerifyAndBuy(id, otp, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Gets the customer cart full information using mysql query.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetCartInfo/{id}")]
        public IHttpActionResult GetCartInfo(int id)
        {
            _crt01Service.GetFullCRT01InfoOfCUS01(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// For buying the single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("BuyItem/{id}")]
        public IHttpActionResult BuyItem(int id)
        {
            _crt01Service.BuySingleItem(id, out Response response);
            return Ok(response);
        }
    }
}
