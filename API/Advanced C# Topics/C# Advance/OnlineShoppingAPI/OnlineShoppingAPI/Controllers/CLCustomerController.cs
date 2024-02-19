using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Customer controller for handling customer api endpoints
    /// </summary>
    [RoutePrefix("api/CLCustomer")]
    [BearerAuth]
    public class CLCustomerController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling customer endpoints.
        /// </summary>
        private readonly IBasicAPIService<CUS01> _customerService;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLCustomerController()
        {
            _customerService = new BLCustomers();
        }

        /// <summary>
        /// Changes the email for an customer.
        /// </summary>
        /// <param name="username">Customer username</param>
        /// <param name="password">Customer old password</param>
        /// <param name="newEmail">Customer new password</param>
        /// <returns>Email change response message</returns>
        [HttpPatch]
        [Route("Change/Email")]
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage ChangeEmail(string username, string password, string newEmail)
            => _customerService.ChangeEmail(username, password, newEmail);

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">Username of the customer</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>HTTP response message</returns>
        [HttpPatch]
        [Route("Change/Password")]
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
            => _customerService.ChangePassword(username, oldPassword, newPassword);

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns>HTTP response message</returns>
        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateCustomer(CUS01 objNewCustomer)
            => _customerService.Create(objNewCustomer);

        /// <summary>
        /// Creates multiple customers.
        /// </summary>
        /// <param name="lstNewCustomers">List of new customers</param>
        /// <returns>HTTP response message</returns>
        [HttpPost]
        [Route("CreateCustomer/List")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateCustomerFromList(List<CUS01> lstNewCustomers)
            => _customerService.CreateFromList(lstNewCustomers);

        /// <summary>
        /// Deletes a customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>HTTP response message</returns>
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage DeleteCustomer(int id)
            => _customerService.Delete(id);

        /// <summary>
        /// Retrieves a list of customers.
        /// </summary>
        /// <returns>HTTP response with customer data</returns>
        [HttpGet]
        [Route("GetCustomers")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCustomers()
            => Ok(_customerService.GetAll());

        /// <summary>
        /// Updates customer information.
        /// </summary>
        /// <param name="objUpdatedCustomer">Updated customer data</param>
        /// <returns>HTTP response message</returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage UpdateCustomer(CUS01 objUpdatedCustomer)
            => _customerService.Update(objUpdatedCustomer);
    }
}
