using CustomWebAPIForDevExpress.Business_Logic;
using Microsoft.AspNetCore.Mvc;

namespace CustomWebAPIForDevExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly UserService userService;

        public JobController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(userService.GetAllJobs());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(userService.GetJob(id));
        }
    }
}
