using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.BL.Service;
using OnlineShoppingAPI.Models;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    /// <summary>
    /// Controller for handling profit-related data.
    /// </summary>
    [RoutePrefix("api/profit")]
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
        /// Initializes a new instance of the CLPFT01Controller class.
        /// </summary>
        public CLPFT01Controller()
        {
            _pft01Service = new BLPFT01();
        }

        /// <summary>
        /// Retrieves profit data for each month.
        /// </summary>
        [HttpGet]
        [Route("monthChartData")]
        public IHttpActionResult GetMonthChartData()
        {
            _pft01Service.GetMonthData(out response);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves aggregated profit data for each year.
        /// </summary>
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
        /// <returns>A list of profit data for each day of the current month.</returns>
        [HttpGet]
        [Route("dayWiseProfit")]
        public IHttpActionResult GetDayWiseProfit()
        {
            _pft01Service.GetDayWiseData(out response);
            return Ok(response);
        }
    }
}
