using OnlineShoppingAPI.Business_Logic;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling profit-related data.
    /// </summary>
    [RoutePrefix("api/profit")]
    public class CLProfitController : ApiController
    {
        /// <summary>
        /// Retrieves monthly profit chart data.
        /// </summary>
        /// <returns>HTTP response with the monthly profit chart data.</returns>
        [HttpGet]
        [Route("monthChartData")]
        public IHttpActionResult GetMonthChartData()
        {
            // Retrieve and return monthly profit chart data using the Business Logic Helper.
            return Ok(BLHelper.GetMonthData());
        }

        /// <summary>
        /// Retrieves profit data for the previous 10 years.
        /// </summary>
        /// <returns>HTTP response with the profit data for the previous 10 years.</returns>
        [HttpGet]
        [Route("yearChartData")]
        public IHttpActionResult GetPrevious10YearData()
        {
            // Retrieve and return profit data for the previous 10 years using the Business Logic Helper.
            return Ok(BLHelper.GetPreviousYearData());
        }

        /// <summary>
        /// Retrieves profit data of this month's
        /// </summary>
        /// <returns>A list of this month's daywise profit</returns>
        [HttpGet]
        [Route("dayWiseProfit")]
        public IHttpActionResult GetDayWiseProfit()
        {
            return Ok(BLHelper.GetDayWiseProfit());
        }
    }
}
