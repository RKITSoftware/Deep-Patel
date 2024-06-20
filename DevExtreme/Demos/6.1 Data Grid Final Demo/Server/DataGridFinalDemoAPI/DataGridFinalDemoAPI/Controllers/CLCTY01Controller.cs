using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGridFinalDemoAPI.Controllers
{
    /// <summary>
    /// City controller for handling city related api's
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLCTY01Controller : ControllerBase
    {
        private readonly ICTY01Service _cty01Service;

        public CLCTY01Controller(ICTY01Service cty01Service)
        {
            _cty01Service = cty01Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_cty01Service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_cty01Service.Get(id));
        }
    }
}
