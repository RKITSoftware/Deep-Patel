using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Record controller for handling api endpoints of order details
    /// </summary>
    [RoutePrefix("api/CLRecord")]
    [BasicAuth]
    public class CLRecordController : ApiController
    {
        /// <summary>
        /// Endpoint :- api/CLRecord/AddOrderDetail
        /// Adding order information of product which customer buys it which product
        /// </summary>
        /// <param name="objOrder">Oreder record of customer</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOrderDetail")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage AddOrder(RCD01 objOrder)
        {
            return BLRecord.Create(objOrder);
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/CreateRecords/List
        /// Adding order information of products which customers buys it which product.
        /// </summary>
        /// <param name="lstNewOrders">Order records of customers</param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateRecords/List")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage CreateOrdersFromList(List<RCD01> lstNewOrders)
        {
            return BLRecord.CreateFromList(lstNewOrders);
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/DeleteRecord/1
        /// Deleting a record of Order Detail
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRecord/{id}")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage DeleteRecord(int id)
        {
            return BLRecord.Delete(id);
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/DownloadCustomerRecords/{id}/{filetype}
        /// Downloading a customer information in Excel and Json format.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="filetype">File type of download</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadCustomerRecords/{id}/{filetype}")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage DownloadCustomerRecord(int id, string filetype)
        {
            return BLRecord.Download(id, filetype);
        }

        /// <summary>
        /// Getting all records details
        /// </summary>
        /// <returns>List of records</returns>
        [HttpGet]
        [Route("GetAllRecordsDetail")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetOrders()
        {
            return Ok(BLRecord.GetAll());
        }

        /// <summary>
        /// Updating record information
        /// </summary>
        /// <param name="objRecord">Updated information of record</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateRecord")]
        [Authorize(Roles = "Customer")]
        public HttpResponseMessage UpdateRecord(RCD01 objRecord)
        {
            return BLRecord.Update(objRecord);
        }
    }
}
