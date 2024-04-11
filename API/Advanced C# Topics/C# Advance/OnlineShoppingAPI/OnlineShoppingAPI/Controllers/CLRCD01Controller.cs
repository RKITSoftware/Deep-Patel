using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Controllers.Filter;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller to handle <see cref="RCD01"/> related api endpoints.
    /// </summary>
    [RoutePrefix("api/CLRCD01")]
    public class CLRCD01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="IRCD01Service"/>.
        /// </summary>
        private readonly IRCD01Service _rcd01Service;

        /// <summary>
        /// Controller to initialize the <see cref="CLPRO02Controller"/>.
        /// </summary>
        public CLRCD01Controller()
        {
            _rcd01Service = new BLRCD01();
        }

        /// <summary>
        /// Add the bought order details into the records table.
        /// </summary>
        /// <param name="objDTORCD01">DTO containing the record information of bought items.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpPost]
        [Route("AddOrderDetail")]
        [Authorize(Roles = "Customer,Admin")]
        [ValidateModel]
        public IHttpActionResult AddOrder(DTORCD01 objDTORCD01)
        {
            _rcd01Service.Operation = EnmOperation.Create;
            if (_rcd01Service.PreValidation(objDTORCD01, out Response response))
            {
                _rcd01Service.PreSave(objDTORCD01);

                if (_rcd01Service.Validation(out response))
                    _rcd01Service.Save(out response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes the record data.
        /// </summary>
        /// <param name="id">Record id.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpDelete]
        [Route("DeleteRecord/{id}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteRecord(int id)
        {
            _rcd01Service.Delete(id, out Response response);
            return Ok(response);
        }

        /// <summary>
        /// Download the customer records.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="filetype">File type that you want to download.</param>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("DownloadCustomerRecords/{id}/{filetype}")]
        [Authorize(Roles = "Customer,Admin")]
        public HttpResponseMessage DownloadCustomerRecord(int id, string filetype)
        {
            return _rcd01Service.Download(id, filetype);
        }

        /// <summary>
        /// Getting all records details
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("GetAllRecordsDetail")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetOrders()
        {
            _rcd01Service.GetAllRecord(out Response response);
            return Ok(response);
        }
    }
}
