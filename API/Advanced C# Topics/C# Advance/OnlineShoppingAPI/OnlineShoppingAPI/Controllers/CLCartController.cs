using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/cart")]
    //[BearerAuth]
    public class CLCartController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling cart endpoints.
        /// </summary>
        private BLCart _cart;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLCartController()
        {
            _cart = new BLCart();
        }

        /// <summary>
        /// Adds an item to the customer's cart.
        /// </summary>
        /// <param name="objProduct">Product information to be added to the cart</param>
        /// <returns>HTTP response message indicating the result of the operation</returns>
        [HttpPost]
        [Route("AddItem")]
        //[ValidateModel]
        public HttpResponseMessage AddItemToCart(CRT01 objProduct)
        {
            return _cart.Add(objProduct);
        }

        /// <summary>
        /// Retrieves customer cart data based on the customer's identifier.
        /// </summary>
        /// <param name="customerId">Identifier of the customer</param>
        /// <returns>HTTP response containing the customer's cart data</returns>
        [HttpGet]
        [Route("{customerId}")]
        public IHttpActionResult GetCustomerCartData(int customerId)
        {
            return Ok(_cart.Get(customerId));
        }

        /// <summary>
        /// Deletes an item from the customer's cart.
        /// </summary>
        /// <param name="cartId">Identifier of the item in the cart to be deleted</param>
        /// <returns>HTTP response message indicating the result of the operation</returns>
        [HttpDelete]
        [Route("DeleteItem/{cartId}")]
        public HttpResponseMessage DeleteItemFromCart(int cartId)
        {
            return _cart.Delete(cartId);
        }

        /// <summary>
        /// Generates an OTP (One-Time Password) for buying items in the cart.
        /// </summary>
        /// <param name="customerId">Identifier of the customer</param>
        /// <returns>HTTP response message containing the OTP</returns>
        [HttpGet]
        [Route("GenerateOtp")]
        public HttpResponseMessage GenerateOtpForBuying(int customerId)
        {
            return _cart.Generate(customerId);
        }

        /// <summary>
        /// Verifies the provided OTP and completes the process of buying items in the cart.
        /// </summary>
        /// <param name="customerId">Identifier of the customer</param>
        /// <param name="otp">One-Time Password for verification</param>
        /// <returns>HTTP response message indicating the result of the operation</returns>
        [HttpPost]
        [Route("VerifyOTP/{customerId}")]
        public HttpResponseMessage VerifyAndBuyItems(int customerId, string otp)
        {
            return _cart.VerifyAndBuy(customerId, otp);
        }

        [HttpGet]
        [Route("GetCartInfo/{id}")]
        public IHttpActionResult GetCartInfo(int id)
        {
            return Ok(_cart.GetCartInfo(id));
        }

        [HttpGet]
        [Route("BuyItem/{id}")]
        public HttpResponseMessage BuyItem(int id)
        {
            return _cart.BuyItem(id);
        }

        #region Unused endpoints

        //[HttpGet]
        //[Route("BuyItems/{customerId}")]
        //public HttpResponseMessage BuyItemsOfCart(int customerId)
        //{
        //    return _cart.BuyAllItems(customerId);
        //}

        #endregion
    }
}
