using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Product controller for handling product api endpoints
    /// </summary>
    [RoutePrefix("api/CLProduct")]
    [BearerAuth]
    public class CLProductController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling product endpoints.
        /// </summary>
        private BLProduct _blProduct;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLProductController()
        {
            _blProduct = new BLProduct();
        }

        /// <summary>>
        /// Endpoint :- api/CLProduct/AddProduct
        /// </summary>
        /// <param name="objNewProduct">Product information</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Create")]
        public HttpResponseMessage AddProduct(PRO01 objNewProduct)
        {
            return _blProduct.Create(objNewProduct);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/CreateProducts/List
        /// </summary>
        /// <param name="lstNewProducts">New products list for create</param>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Create/List")]
        public HttpResponseMessage CreateProductsFromList(List<PRO01> lstNewProducts)
        {
            return _blProduct.CreateFromList(lstNewProducts);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/DeleteProduct/{id}
        /// </summary>
        /// <param name="id">Product id</param>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteProduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            return _blProduct.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/GetProducts
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [Route("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            return Ok(_blProduct.GetAll());
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/UpdateProduct
        /// </summary>
        /// <param name="objUpdatedProduct">Updated product data</param>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("UpdateProduct")]
        public HttpResponseMessage UpdateProduct(PRO01 objUpdatedProduct)
        {
            return _blProduct.Update(objUpdatedProduct);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/UpdateQuantity/1
        /// </summary>
        /// <param name="productId">Product id for finding product.</param>
        /// <param name="quantity">Quantity that you want to add.</param>
        [HttpPatch]
        [Route("UpdateQuantity/{productId}")]
        public HttpResponseMessage UpdateProductQuantity(int productId, int quantity)
        {
            return _blProduct.UpdateQuantity(productId, quantity);
        }
    }
}
