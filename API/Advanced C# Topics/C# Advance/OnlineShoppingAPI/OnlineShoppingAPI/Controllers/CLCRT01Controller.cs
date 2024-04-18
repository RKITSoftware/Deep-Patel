using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Controllers.Attribute;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
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
            _crt01Service = new BLCRT01Handler();
        }

        /// <summary>
        /// Add item to the customer cart.
        /// </summary>
        /// <param name="objDTOCRT01">DTO containing the details.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("AddItem")]
        [ValidateModel]
        public IHttpActionResult AddItemToCart(DTOCRT01 objDTOCRT01)
        {
            _crt01Service.Operation = EnmOperation.A;
            Response response = _crt01Service.PreValidation(objDTOCRT01);

            if (!response.IsError)
            {
                _crt01Service.PreSave(objDTOCRT01);
                response = _crt01Service.Validation();

                if (!response.IsError)
                    response = _crt01Service.Save();
            }

            return Ok(response);
        }

        /// <summary>
        /// For buying the single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("BuyItem/{id}")]
        [Authorize(Roles = "Customer")]
        public IHttpActionResult BuyItem(int id)
        {
            Response response = _crt01Service.BuySingleItem(id);
            return Ok(response);
        }

        /// <summary>
        /// Deleted the item from the customer cart.
        /// </summary>
        /// <param name="id">Cart Id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("DeleteItem/{id}")]
        [Authorize(Roles = "Customer")]
        public IHttpActionResult DeleteItemFromCart(int id)
        {
            Response response = _crt01Service.Delete(id);
            return Ok(response);
        }

        /// <summary>
        /// Generates the OTP for 2-Factor Buy Process.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GenerateOtp")]
        [Authorize(Roles = "Customer")]
        public IHttpActionResult GenerateOtpForBuying(int id)
        {
            Response response = _crt01Service.Generate(id);
            return Ok(response);
        }

        /// <summary>
        /// Gets the customer cart full information using mysql query.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetCartInfo/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public IHttpActionResult GetCartInfo(int id)
        {
            Response response = _crt01Service.GetFullCRT01InfoOfCUS01(id);
            return Ok(response);
        }

        /// <summary>
        /// Retrieve the customer cart information using Orm Lite.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public IHttpActionResult GetCustomerCartData(int id)
        {
            Response response = _crt01Service.GetCUS01CRT01Details(id);
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
        [Authorize(Roles = "Customer")]
        public IHttpActionResult VerifyAndBuyItems(int id, string otp)
        {
            Response response = _crt01Service.VerifyAndBuy(id, otp);
            return Ok(response);
        }
    }
}
