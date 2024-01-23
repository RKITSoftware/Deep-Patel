using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using System.Web;
using System;
using System.Web.Http;
using ServiceStack.OrmLite;
using System.Collections.Generic;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/[controller]/[action]/[id]")]
    public class CLProductController : ApiController
    {
        private readonly IDbConnectionFactory _dbFactory;

        public CLProductController()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/AddProduct
        /// Adding new product to the Products table
        /// </summary>
        /// <param name="objNewProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddProduct(PRO01 objNewProduct)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    db.CreateTable<PRO01>();

                db.Insert<PRO01>(objNewProduct);
                return Ok("Product added successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/GetProducts
        /// Getting all the products of online shopping app
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    return NotFound();

                var products = db.Select<PRO01>();
                return Ok(products);
            }
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/CreateProduct/List
        /// Creating products using a list of products data.
        /// </summary>
        /// <param name="lstNewProducts">New products list</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpPost]
        [Route("CreateProducts/List")]
        public IHttpActionResult CreateProductsFromList(List<PRO01> lstNewProducts)
        {
            if (lstNewProducts.Count == 0)
                return BadRequest("Data is empty");

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    db.CreateTable<PRO01>();

                db.InsertAll<PRO01>(lstNewProducts);
                return Ok("Products created successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLProducts/DeleteProduct/1
        /// Deleting a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be zero or negative.");

            using (var db = _dbFactory.OpenDbConnection())
            {
                var customer = db.SingleById<PRO01>(id);

                if (customer == null)
                    return NotFound();

                db.Delete<PRO01>(id);
                return Ok("Product deleted successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLProduct/UpdateCustomer
        /// For updating product details of product
        /// </summary>
        /// <param name="objUpdatedProduct">updated product data</param>
        /// <returns>Ok or BadRequest or NotFound response</returns>
        [HttpPut]
        [Route("UpdateProduct")]
        public IHttpActionResult UpdateProduct(PRO01 objUpdatedProduct)
        {
            if (objUpdatedProduct.O01F01 <= 0)
                return BadRequest("Id can't be zero or negative.");

            using (var db = _dbFactory.OpenDbConnection())
            {
                PRO01 existingProduct = db.SingleById<PRO01>(objUpdatedProduct.O01F01);

                if (existingProduct == null)
                    return NotFound();

                existingProduct.O01F02 = objUpdatedProduct.O01F02;
                existingProduct.O01F03 = objUpdatedProduct.O01F03;
                existingProduct.O01F04 = objUpdatedProduct.O01F04;
                existingProduct.O01F05 = objUpdatedProduct.O01F05;
                
                return Ok("Product updated successfully.");
            }
        }
    }
}
