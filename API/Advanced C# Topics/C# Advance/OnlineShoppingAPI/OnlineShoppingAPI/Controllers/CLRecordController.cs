using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Record controller for handling api endpoints of order details
    /// </summary>
    [RoutePrefix("api/CLRecord")]
    [BearerAuth]
    public class CLRecordController : ApiController
    {
        /// <summary>
        /// Business logic class instance for handling record endpoints.
        /// </summary>
        private BLRecord _blRecord;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLRecordController()
        {
            _blRecord = new BLRecord();
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/AddOrderDetail
        /// Adding order information of product which customer buys it which product
        /// </summary>
        /// <param name="objOrder">Oreder record of customer</param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOrderDetail")]
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage AddOrder(RCD01 objOrder)
        {
            return _blRecord.Create(objOrder);
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/CreateRecords/List
        /// Adding order information of products which customers buys it which product.
        /// </summary>
        /// <param name="lstNewOrders">Order records of customers</param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("CreateRecords/List")]
        //[Authorize(Roles = "Customer")]
        //public HttpResponseMessage CreateOrdersFromList(List<RCD01> lstNewOrders)
        //{
        //    return _blRecord.CreateFromList(lstNewOrders);
        //}

        /// <summary>
        /// Endpoint :- api/CLRecord/DeleteRecord/1
        /// Deleting a record of Order Detail
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteRecord/{id}")]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage DeleteRecord(int id)
        {
            return _blRecord.Delete(id);
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
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage DownloadCustomerRecord(int id, string filetype)
        {
            return _blRecord.Download(id, filetype);
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
            return Ok(_blRecord.GetAll());
        }

        /// <summary>
        /// Endpoint :- api/CLRecord/UpdateRecord
        /// Updating record information
        /// </summary>
        /// <param name="objRecord">Updated information of record</param>
        /// <returns></returns>
        //[HttpPut]
        //[Route("UpdateRecord")]
        //[Authorize(Roles = "Admin")]
        //public HttpResponseMessage UpdateRecord(RCD01 objRecord)
        //{
        //    _blRecord = new BLRecord();
        //    return _blRecord.Update(objRecord);
        //}
    }
}
