using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLCustomer")]
    public class CLCustomerController : ApiController
    {
        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer
        /// Adding new customer to the customer table
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns>Customer created successfully.</returns>
        [HttpPost]
        [Route("CreateCustomer")]
        public HttpResponseMessage CreateCustomer(CUS01 objNewCustomer)
        {
            return BLCustomers.Create(objNewCustomer);
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
            return Ok(BLCustomers.GetData());
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer/List
        /// Creating customers using a list of customer data.
        /// </summary>
        /// <param name="lstNewCustomers">New customer list</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpPost]
        [Route("CreateCustomer/List")]
        public HttpResponseMessage CreateCustomerFromList(List<CUS01> lstNewCustomers)
        {
            return BLCustomers.CreateFromList(lstNewCustomers);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/DeleteCustomer/1
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Ok or BadRequest response</returns>
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            return BLCustomers.DeleteData(id);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/UpdateCustomer
        /// For updating customer details of customer
        /// </summary>
        /// <param name="objUpdatedCustomer">updated customer data</param>
        /// <returns>Ok or BadRequest or NotFound repsonse</returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        public HttpResponseMessage UpdateCustomer(CUS01 objUpdatedCustomer)
        {
            return BLCustomers.UpdateData(objUpdatedCustomer);
        }
    }
}
