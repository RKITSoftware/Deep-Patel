using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Product controller for handling product API endpoints.
    /// </summary>
    [RoutePrefix("api/CLProduct")]
    //[CookieBasedAuth]
    public class CLProductController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling product endpoints.
        /// </summary>
        private BLProduct _blProduct;

        /// <summary>
        /// Business logic class instance for handling product version 2 endpoints.
        /// </summary>
        private BLProductV2 _blProductV2;

        /// <summary>
        /// Constructor to initialize the Business Logic instances.
        /// </summary>
        public CLProductController()
        {
            _blProduct = new BLProduct();
            _blProductV2 = new BLProductV2();
        }

        /// <summary>
        /// Endpoint: api/CLProduct/Create
        /// </summary>
        /// <param name="objNewProduct">Product information</param>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Route("Create")]
        //[ValidateModel]
        public HttpResponseMessage AddProduct(PRO01 objNewProduct)
        {
            return _blProduct.Create(objNewProduct);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/Create/List
        /// </summary>
        /// <param name="lstNewProducts">New products list for creation</param>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [Route("Create/List")]
        //[ValidateModel]
        public HttpResponseMessage CreateProductsFromList(List<PRO01> lstNewProducts)
        {
            return _blProduct.CreateFromList(lstNewProducts);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/DeleteProduct/{id}
        /// </summary>
        /// <param name="id">Product ID</param>
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("DeleteProduct/{id}")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            return _blProduct.Delete(id);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/GetProducts
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        [Route("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            return Ok(_blProduct.GetAll());
        }

        /// <summary>
        /// Endpoint: api/CLProduct/UpdateProduct
        /// </summary>
        /// <param name="objUpdatedProduct">Updated product data</param>
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        [Route("UpdateProduct")]
        //[ValidateModel]
        public HttpResponseMessage UpdateProduct(PRO01 objUpdatedProduct)
        {
            return _blProduct.Update(objUpdatedProduct);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/UpdateQuantity/{productId}
        /// </summary>
        /// <param name="productId">Product ID for finding the product</param>
        /// <param name="quantity">Quantity to add</param>
        [HttpPatch]
        [Route("UpdateQuantity/{productId}")]
        public HttpResponseMessage UpdateProductQuantity(int productId, int quantity)
        {
            return _blProduct.UpdateQuantity(productId, quantity);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/Add
        /// </summary>
        /// <param name="objProduct">Product information</param>
        [HttpPost]
        [Route("Add")]
        //[ValidateModel]
        public HttpResponseMessage Add(PRO02 objProduct)
        {
            return _blProductV2.Add(objProduct);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/DeleteProductV2
        /// </summary>
        /// <param name="productId">Product ID</param>
        [HttpDelete]
        [Route("DeleteProductV2")]
        public HttpResponseMessage DeleteProductV2(int productId)
        {
            return _blProductV2.Delete(productId);
        }

        /// <summary>
        /// Endpoint: api/CLProduct/GetProductV2
        /// </summary>
        /// <returns>List of products (version 2)</returns>
        [HttpGet]
        [Route("GetProductV2")]
        public IHttpActionResult GetPRO02()
        {
            return Ok(_blProductV2.GetAll());
        }

        /// <summary>
        /// Endpoint: api/CLProduct/UpdateSellPrice
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="sellPrice">New sell price for the product</param>
        [HttpPatch]
        [Route("UpdateSellPrice")]
        public HttpResponseMessage UpdateSellPrice(int productId, int sellPrice)
        {
            return _blProductV2.UpdateSellPrice(productId, sellPrice);
        }

        [HttpGet]
        [Route("GetAllProductsInfo")]
        public IHttpActionResult GetAllProductInfo()
        {
            return Ok(_blProductV2.GetInfo());
        }
    }
}
