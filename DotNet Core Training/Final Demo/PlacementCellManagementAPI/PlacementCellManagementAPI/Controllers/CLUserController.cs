using Microsoft.AspNetCore.Mvc;

namespace PlacementCellManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUserController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
