using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling <see cref="PFT01"/> api endpoints.
    /// </summary>
    [RoutePrefix("api/CLPFT01")]
    [Authorize(Roles = "Admin")]
    public class CLPFT01Controller : ApiController
    {
        /// <summary>
        /// Services of <see cref="IPFT01Service"/>.
        /// </summary>
        private readonly IPFT01Service _pft01Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLPFT01Controller"/> class.
        /// </summary>
        public CLPFT01Controller()
        {
            _pft01Service = new BLPFT01Handler();
        }

        /// <summary>
        /// Retrieves profit data for each day of the current month.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("dayWiseProfit")]
        public IHttpActionResult GetDayWiseProfit()
        {
            return Ok(_pft01Service.GetDayWiseData());
        }

        /// <summary>
        /// Retrieves profit data for each month of current year.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("monthChartData")]
        public IHttpActionResult GetMonthChartData()
        {
            return Ok(_pft01Service.GetMonthData());
        }

        /// <summary>
        /// Retrieves aggregated profit data for last 10 year.
        /// </summary>
        /// <returns><see cref="Response"/> containing the output of the HTTP request.</returns>
        [HttpGet]
        [Route("yearChartData")]
        public IHttpActionResult GetPrevious10YearData()
        {
            return Ok(_pft01Service.GetYearData());
        }
    }
}