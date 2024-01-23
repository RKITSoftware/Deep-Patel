using ServiceStack.Data;
using System.Web;
using System;
using System.Web.Http;
using OnlineShoppinAPI.Models;
using ServiceStack.OrmLite;

namespace OnlineShoppinAPI.Controllers
{
    [RoutePrefix("api/CLCustomer")]
    public class CLCustomerController : ApiController
    {
        private readonly IDbConnectionFactory _dbFactory;

        public CLCustomerController()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer
        /// Adding new customer to the customer table
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns>Customer created successfully.</returns>
        [HttpPost]
        public IHttpActionResult CreateCustomer(CUS01 objNewCustomer)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    db.CreateTable<CUS01>();

                db.Insert<CUS01>(objNewCustomer);
                return Ok("Customer created successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/GetCustomers
        /// Getting all the customer details of online shopping app
        /// </summary>
        /// <returns>Customer details</returns>
        [HttpGet]
        [Route("GetCustomers")]
        public IHttpActionResult GetCustomers()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    return NotFound();

                var customers = db.Select<CUS01>();
                return Ok(customers);
            }
        }
    }
}
