using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataGridFinalDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLEDC01Controller : ControllerBase
    {
        private readonly IEDC01Service _edc01Service;

        public CLEDC01Controller(IEDC01Service edc01Service)
        {
            _edc01Service = edc01Service;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetEducationOfStudent(int id)
        {
            return Ok(_edc01Service.GetAll(id));
        }
    }
}
