using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/cart")]
    public class CLCartController : ApiController
    {
        private BLCart _cart;

        public CLCartController()
        {
            _cart = new BLCart();
        }

        [HttpPost]
        [Route("AddItem")]
        public HttpResponseMessage AddItemToCart(CRT01 objProduct)
        {
            return _cart.Add(objProduct);
        }

        [HttpDelete]
        [Route("DeleteItem/{cartId}")]
        public HttpResponseMessage DeleteItemFromCart(int cartId)
        {
            return _cart.Delete(cartId);
        }

        [HttpGet]
        [Route("{customerId}")]
        public IHttpActionResult GetCustomerCartData(int customerId)
        {
            return Ok(_cart.Get(customerId));
        }
    }
}
