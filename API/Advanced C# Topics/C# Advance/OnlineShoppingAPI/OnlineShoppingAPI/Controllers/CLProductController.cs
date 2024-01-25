using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLProduct")]
    [BasicAuth]
    [Authorize(Roles = "Customer")]
    public class CLProductController : ApiController
    {
        /// <summary>>
        /// Endpoint :- api/CLProduct/AddProduct
        /// Adding new product to the Products table
        /// </summary>
        /// <param name="objNewProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProduct")]
        public HttpResponseMessage AddProduct(PRO01 objNewProduct)
        {
            return BLProduct.Create(objNewProduct);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/GetProducts
        /// Getting all the products of online shopping app
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet]
        [Route("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            return Ok(BLProduct.GetAll());
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/CreateProduct/List
        /// Creating products using a list of products data.
        /// </summary>
        /// <param name="lstNewProducts">New products list</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpPost]
        [Route("CreateProducts/List")]
        public HttpResponseMessage CreateProductsFromList(List<PRO01> lstNewProducts)
        {
            return BLProduct.CreateFromList(lstNewProducts);
        }

        /// <summary>
        /// Endpoint :- api/CLProducts/DeleteProduct/1
        /// Deleting a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            return BLProduct.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/UpdateCustomer
        /// For updating product details of product
        /// </summary>
        /// <param name="objUpdatedProduct">updated product data</param>
        /// <returns>Ok or BadRequest or NotFound response</returns>
        [HttpPut]
        [Route("UpdateProduct")]
        public HttpResponseMessage UpdateProduct(PRO01 objUpdatedProduct)
        {
            return BLProduct.Update(objUpdatedProduct);
        }
    }
}
