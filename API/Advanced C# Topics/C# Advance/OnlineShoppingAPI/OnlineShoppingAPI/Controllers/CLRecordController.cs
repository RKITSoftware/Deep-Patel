using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Security;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLRecord")]
    [BasicAuth]
    [Authorize(Roles = "Admin")]
    public class CLRecordController : ApiController
    {
        [HttpPost]
        [Route("AddOrderDetail")]
        public HttpResponseMessage AddOrder(RCD01 objOrder)
        {
            return BLRecord.Create(objOrder);
        }

        [HttpPost]
        [Route("CreateRecords/List")]
        public HttpResponseMessage CreateOrdersFromList(List<RCD01> lstNewOrders)
        {
            return BLRecord.CreateFromList(lstNewOrders);
        }

        [HttpDelete]
        [Route("DeleteRecord/{id}")]
        public HttpResponseMessage DeleteRecord(int id)
        {
            return BLRecord.Delete(id);
        }

        [HttpGet]
        [Route("DownloadCustomerRecords/{id}/{filetype}")]
        public HttpResponseMessage DownloadCustomerRecord(int id, string filetype)
        {
            return BLRecord.Download(id, filetype);
        }

        [HttpGet]
        [Route("GetAllRecordsDetail")]
        public IHttpActionResult GetOrders()
        {
            return Ok(BLRecord.GetAll());
        }

        [HttpPut]
        [Route("UpdateRecord")]
        public HttpResponseMessage UpdateRecord(RCD01 objRecord)
        {
            return BLRecord.Update(objRecord);
        }
    }
}
