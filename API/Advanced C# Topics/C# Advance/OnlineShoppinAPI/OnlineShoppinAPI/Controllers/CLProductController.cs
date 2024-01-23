using OnlineShoppinAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Web;
using System;
using System.Web.Http;

namespace OnlineShoppinAPI.Controllers
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
    }
}
