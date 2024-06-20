using DataGridFinalDemoAPI.Business_Logic.Interfaces;
using DataGridFinalDemoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataGridFinalDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLSTU01Controller : ControllerBase
    {
        private readonly ISTU01Service _stu01Service;

        public CLSTU01Controller(ISTU01Service stu01Service)
        {
            _stu01Service = stu01Service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_stu01Service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(STU01 objSTU01)
        {
            if (_stu01Service.Add(objSTU01))
                return Ok("Successfully Added.");

            return BadRequest("Error Occured.");
        }

        [HttpPut]
        public IActionResult Update(STU01 objSTU01)
        {
            if (_stu01Service.Update(objSTU01))
                return Ok("Successfully Updated.");

            return BadRequest("Error Occured.");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (_stu01Service.Delete(id))
                return Ok("Successfully Deletd.");

            return BadRequest("Error Occured.");
        }

        [HttpGet("ValidateMN")]
        public IActionResult ValidateMobileNumber([FromQuery] int id, [FromQuery] string number)
        {
            return Ok(new { isValid = _stu01Service.ValidateMobileNumber(id, number)});
        }
    }
}
