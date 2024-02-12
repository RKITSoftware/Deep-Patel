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
    [BasicAuth]
    public class CLProductController : ApiController
    {
        private BLProduct _blProduct;

        public CLProductController()
        {
            _blProduct = new BLProduct();
        }

        /// <summary>>
        /// Endpoint :- api/CLProduct/AddProduct
        /// </summary>
        /// <param name="objNewProduct">Product information</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("AddProduct")]
        public HttpResponseMessage AddProduct(PRO01 objNewProduct)
        {
            return _blProduct.Create(objNewProduct);
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
        /// Endpoint :- api/CLProduct/CreateProducts/List
        /// </summary>
        /// <param name="lstNewProducts">New products list for create</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("CreateProducts/List")]
        public HttpResponseMessage CreateProductsFromList(List<PRO01> lstNewProducts)
        {
            return _blProduct.CreateFromList(lstNewProducts);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/DeleteProduct/{id}
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("DeleteProduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            return _blProduct.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/UpdateProduct
        /// </summary>
        /// <param name="objUpdatedProduct">Updated product data</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("UpdateProduct")]
        public HttpResponseMessage UpdateProduct(PRO01 objUpdatedProduct)
        {
            return _blProduct.Update(objUpdatedProduct);
        }
    }
}
