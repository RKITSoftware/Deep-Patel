using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorizeDemo.Controllers
{
    /// <summary>
    /// User controller for handling user-related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLUserController : ControllerBase
    {
        [HttpGet("LogIn")]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return Ok("Successfully Logged in.");
        }
    }
}
