using ORMToolDemo.Business_Logic;
using ORMToolDemo.Models;
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
        /// Business logic of Customer Controller Instance
        /// </summary>
        private readonly BLCustomer _blCustomer;

        /// <summary>
        /// Initializing the <see cref="CLCustomerController"/> instances.
        /// </summary>
        public CLCustomerController()
        {
            _blCustomer = new BLCustomer();
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
            return Ok(_blCustomer.GetAll());
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
            return Ok(_blCustomer.GetById(id));
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
            return Ok(_blCustomer.Add(objCustomer));
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
            return Ok(_blCustomer.Delete(id));
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
            return Ok(_blCustomer.Update(objUpdatedCustomer));
        }
    }
}
