using ORMToolDemo.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Web;
using System.Web.Http;

namespace ORMToolDemo.Controllers
{
    /// <summary>
    /// Customer Controller for handling API endpoints using database with ormlite tool.
    /// </summary>
    [RoutePrefix("api/CLCustomer")]
    public class CLCustomerController : ApiController
    {
        /// <summary>
        /// Database connection interface for CRUD operation
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Giving a database reference to dbfactory
        /// </summary>
        /// <exception cref="ApplicationException">If connection with database not establish 
        /// then it gives erro.</exception>
        public CLCustomerController()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// GET :- api/CLCustomer/GetCustomers
        /// For getting all customer details from database.
        /// </summary>
        /// <returns>All customer details.</returns>
        [HttpGet]
        [Route("GetCustomers")]
        public IHttpActionResult GetCustomers()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var customers = db.Select<Customer>();
                return Ok(customers);
            }
        }

        /// <summary>
        /// GET :- api/CLCustomer/GetCustomer/1
        /// For getting a specific customer using his id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns>Customer</returns>
        [HttpGet]
        [Route("GetCustomer/{id}")]
        public IHttpActionResult GetCustomerById(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var customer = db.SingleById<Customer>(id);
                return Ok(customer);
            }
        }

        /// <summary>
        /// POST :- api/CLCustomer/Add
        /// Adding customer data into database
        /// </summary>
        /// <param name="objCustomer">Customer data</param>
        /// <returns>Response message</returns>
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult AddData(Customer objCustomer)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(objCustomer);
                return Ok("Added Successfully");
            }
        }

        /// <summary>
        /// DELETE :- api/CLCustomer/Delete/1
        /// Delete a customer data using id.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Delete response</returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public IHttpActionResult DeleteData(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<Customer>(id);
                return Ok("Delete");
            }
        }

        /// <summary>
        /// PUT :- api/CLCustomer/Update/1
        /// For updating a specific customer data using customer id.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="objUpdatedCustomer">Updated data of Customer</param>
        /// <returns>Response message</returns>
        [HttpPut]
        [Route("Update/{id}")]
        public IHttpActionResult PutCustomer(int id, Customer objUpdatedCustomer)
        {
            if (objUpdatedCustomer == null)
            {
                return BadRequest("Invalid customer data.");
            }

            using (var db = _dbFactory.OpenDbConnection())
            {
                var objExistingCustomer = db.SingleById<Customer>(id);

                if (objExistingCustomer == null)
                    return NotFound();

                objExistingCustomer.FirstName = objUpdatedCustomer.FirstName;
                objExistingCustomer.LastName = objUpdatedCustomer.LastName;

                db.Update(objExistingCustomer);

                return Ok("Customer updated successfully");
            }
        }
    }
}
