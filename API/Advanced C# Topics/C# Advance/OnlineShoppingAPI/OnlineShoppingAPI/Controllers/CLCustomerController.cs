using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
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
        [Route("CreateCustomer")]
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

        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer/List
        /// Creating customers using a list of customer data.
        /// </summary>
        /// <param name="lstNewCustomers">New customer list</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpPost]
        [Route("CreateCustomer/List")]
        public IHttpActionResult CreateCustomerFromList(List<CUS01> lstNewCustomers)
        {
            if (lstNewCustomers.Count == 0)
                return BadRequest("Data is empty");

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    db.CreateTable<CUS01>();

                db.InsertAll<CUS01>(lstNewCustomers);
                return Ok("Customers created successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/DeleteCustomer/1
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            if (id <= 0)
                return BadRequest("Id can't be zero or negative.");

            using (var db = _dbFactory.OpenDbConnection())
            {
                var customer = db.SingleById<CUS01>(id);

                if (customer == null)
                    return NotFound();

                db.Delete<CUS01>(id);
                return Ok("Customer deleted successfully.");
            }
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/UpdateCustomer
        /// For updating customer details of customer
        /// </summary>
        /// <param name="objUpdatedCustomer">updated customer data</param>
        /// <returns>Ok or BadRequest or NotFound repsonse</returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        public IHttpActionResult UpdateCustomer(CUS01 objUpdatedCustomer)
        {
            if (objUpdatedCustomer.S01F01 <= 0)
                return BadRequest("Id can't be zero or negative.");

            using (var db = _dbFactory.OpenDbConnection())
            {
                CUS01 existingCustomer = db.SingleById<CUS01>(objUpdatedCustomer.S01F01);

                if (existingCustomer == null)
                    return NotFound();

                existingCustomer.S01F02 = objUpdatedCustomer.S01F02;
                existingCustomer.S01F03 = objUpdatedCustomer.S01F03;
                existingCustomer.S01F04 = objUpdatedCustomer.S01F04;
                existingCustomer.S01F05 = objUpdatedCustomer.S01F05;

                return Ok("Customer updated successfully.");
            }
        }
    }
}
