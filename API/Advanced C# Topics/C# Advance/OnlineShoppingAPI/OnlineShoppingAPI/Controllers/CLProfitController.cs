using OnlineShoppingAPI.Business_Logic;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    [RoutePrefix("api/profit")]
    public class CLProfitController : ApiController
    {
        [HttpGet]
        [Route("monthChartData")]
        public IHttpActionResult GetMonthChartData()
        {
            return Ok(BLHelper.GetMonthData());
        }

        [HttpGet]
        [Route("yearChartData")]
        public IHttpActionResult GetPrevious10YearData()
        {
            return Ok(BLHelper.GetPreviousYearData());
        }
    }
}
