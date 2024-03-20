using Microsoft.AspNetCore.Mvc;
using MiddleWareDemo.Business_Logic;
using MiddleWareDemo.Model;

namespace MiddleWareDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BLUser _blUser;

        public UserController()
        {
            _blUser = new BLUser();
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_blUser.Get());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public USR01 Get(int id)
        {
            return _blUser.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post(USR01 objUser)
        {
            _blUser.Add(objUser);
            return StatusCode(201, "User created successfully.");
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blUser.Delete(id);
            return Ok("User deleted successfully.");
        }
    }
}
