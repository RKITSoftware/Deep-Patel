using CustomWebAPIForDevExpress.Business_Logic;
using CustomWebAPIForDevExpress.Models;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace CustomWebAPIForDevExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] DataSourceLoadOptionsBase loadOptionsBase)
        {
            var result = DataSourceLoader.Load(userService.GetAll(), loadOptionsBase);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(userService.Delete(id));
        }

        [HttpPost]
        public IActionResult PostUser(User user)
        {
            return Ok(userService.AddUser(user));
        }

        [HttpPut]
        public IActionResult UpdateUser(User objUser)
        {
            if (!userService.UpdateDetails(objUser))
            {
                return NotFound();
            }

            return Ok("Successfully updated.");
        }
    }
}
