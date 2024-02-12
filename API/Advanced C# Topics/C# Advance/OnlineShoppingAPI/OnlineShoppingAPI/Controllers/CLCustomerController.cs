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
    [BasicAuth]
    public class CLCustomerController : ApiController
    {
        private readonly IBasicAPIService<CUS01> _customerService;

        public CLCustomerController()
        {
            _customerService = new BLCustomers();
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateCustomer(CUS01 objNewCustomer)
        {
            return _customerService.Create(objNewCustomer);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/GetCustomers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCustomers")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCustomers()
        {
            return Ok(_customerService.GetAll());
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/CreateCustomer/List
        /// </summary>
        /// <param name="lstNewCustomers">New customer list</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateCustomer/List")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CreateCustomerFromList(List<CUS01> lstNewCustomers)
        {
            return _customerService.CreateFromList(lstNewCustomers);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/DeleteCustomer/{id}
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            return _customerService.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/UpdateCustomer
        /// </summary>
        /// <param name="objUpdatedCustomer">updated customer data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage UpdateCustomer(CUS01 objUpdatedCustomer)
        {
            return _customerService.Update(objUpdatedCustomer);
        }

        /// <summary>
        /// Endpoint :- api/CLCustomer/ChangePassword
        /// </summary>
        /// <param name="username">User name of user</param>
        /// <param name="newPassword">New password of user</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("ChangePassword")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            return _customerService.ChangePassword(username, oldPassword, newPassword);
        }
    }
}
