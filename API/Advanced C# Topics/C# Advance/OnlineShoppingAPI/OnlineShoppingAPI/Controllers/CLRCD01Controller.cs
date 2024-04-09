using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/CLRCD01")]
    //[BearerAuth]
    public class CLRecordController : ApiController
    {
        private readonly IRCD01Service _rcd01Service;

        public CLRecordController()
        {
            _rcd01Service = new BLRCD01();
        }

        [HttpPost]
        [Route("AddOrderDetail")]
        //[Authorize(Roles = "Customer,Admin")]
        //[ValidateModel]
        public IHttpActionResult AddOrder(DTORCD01 objDTORCD01)
        {
            Response response;
            _rcd01Service.PreSave(objDTORCD01, EnmOperation.Create);

            if (_rcd01Service.Validation(out response))
            {
                _rcd01Service.Save(out response);
            }

            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteRecord/{id}")]
        // [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteRecord(int id)
        {
            Response response;
            _rcd01Service.Delete(id, out response);

            return Ok(response);
        }

        [HttpGet]
        [Route("DownloadCustomerRecords/{id}/{filetype}")]
        // [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage DownloadCustomerRecord(int id, string filetype)
        {
            return _rcd01Service.Download(id, filetype);
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
            Response response;
            _rcd01Service.GetAllRecord(out response);

            return Ok(response);
        }
    }
}
