using OnlineShoppingAPI.Business_Logic;
using System.Web.Http;

namespace OnlineShoppingAPI.Controllers
{
    public class CLProfitController : ApiController
    {
        [HttpGet]
        [Route("monthChartData/{year}")]
        public IHttpActionResult GetMonthChartData(int year)
        {
            return Ok(BLHelper.GetMonthData(year));
        }
    }
}
