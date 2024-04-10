﻿using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Attribute;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Customer controller for handling <see cref="CUS01"/> api endpoints.
    /// </summary>
    [RoutePrefix("api/CLCUS01")]
    [BearerAuth]
    public class CLCUS01Controller : ApiController
    {
        /// <summary>
        /// Services for <see cref="CLCUS01Controller"/>
        /// </summary>
        private readonly ICUS01Service _cus01Service;

        /// <summary>
        /// Constructor to initialize the <see cref="CLCUS01Controller"/>
        /// </summary>
        public CLCUS01Controller()
        {
            _cus01Service = new BLCUS01();
        }

        /// <summary>
        /// Changes the email of customer.
        /// </summary>
        /// <param name="username">Customer's username.</param>
        /// <param name="password">Customer's current password.</param>
        /// <param name="newEmail">Customer's new email Id.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPatch]
        [Route("Change/Email")]
        [Authorize(Roles = "Customer,Admin")]
        public IHttpActionResult ChangeEmail(string username, string password, string newEmail)
        {
            _cus01Service.ChangeEmail(username, password, newEmail, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Change the password of customer.
        /// </summary>
        /// <param name="username">Customer's username.</param>
        /// <param name="oldPassword">Customer's current password.</param>
        /// <param name="newPassword">Customer's new password.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPatch]
        [Route("Change/Password")]
        [Authorize(Roles = "Customer,Admin")]
        public IHttpActionResult ChangePassword(string username, string oldPassword, string newPassword)
        {
            _cus01Service.ChangePassword(username, oldPassword, newPassword, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="objCUS01DTO">DTO of CUS01 for Creating.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPost]
        [Route("CreateCustomer")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IHttpActionResult CreateCustomer(DTOCUS01 objCUS01DTO)
        {
            _cus01Service.PreSave(objCUS01DTO, EnmOperation.Create);

            if (_cus01Service.Validation(out Response response))
            {
                _cus01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes the custome using Id
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            _cus01Service.Delete(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Get all customer data.
        /// </summary>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpGet]
        [Route("GetCustomers")]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult GetCustomers()
        {
            _cus01Service.GetAll(out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Get the customer data using customer id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetCustomerById(int id)
        {
            _cus01Service.GetById(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Update the customer details.
        /// </summary>
        /// <param name="objCUS01DTO">DTO for Updating CUS01 Details.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        [HttpPut]
        [Route("UpdateCustomer")]
        [Authorize(Roles = "Customer,Admin")]
        [ValidateModel]
        public IHttpActionResult UpdateCustomer(DTOCUS01 objCUS01DTO)
        {
            _cus01Service.PreSave(objCUS01DTO, EnmOperation.Update);

            if (_cus01Service.Validation(out Response response))
            {
                _cus01Service.Save(out response);
            }

            return Ok(response);
        }
    }
}
