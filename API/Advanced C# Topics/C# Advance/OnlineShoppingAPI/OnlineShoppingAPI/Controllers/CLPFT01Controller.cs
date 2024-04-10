using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="PFT01"/> api endpoints.
    /// </summary>
    [RoutePrefix("api/CLPFT01")]
    public class CLPFT01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="IPFT01Service"/>.
        /// </summary>
        private readonly IPFT01Service _pft01Service;

        /// <summary>
        /// Response object contains the informations about request's response.
        /// </summary>
        private Response response;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLPFT01Controller"/> class.
        /// </summary>
        public CLPFT01Controller()
        {
            _pft01Service = new BLPFT01();
        }

        /// <summary>
        /// Retrieves profit data for each month of current year.
        /// </summary>        
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("monthChartData")]
        public IHttpActionResult GetMonthChartData()
        {
            _pft01Service.GetMonthData(out response);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves aggregated profit data for last 10 year.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("yearChartData")]
        public IHttpActionResult GetPrevious10YearData()
        {
            _pft01Service.GetYearData(out response);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves profit data for each day of the current month.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("dayWiseProfit")]
        public IHttpActionResult GetDayWiseProfit()
        {
            _pft01Service.GetDayWiseData(out response);
            return Ok(response);
        }
    }
}
