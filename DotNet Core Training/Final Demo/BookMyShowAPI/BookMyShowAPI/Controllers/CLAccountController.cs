using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowAPI.Controllers
{
    /// <summary>
    /// Account controller for handling user's login, sign-in and other password related api's.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLAccountController : ControllerBase
    {
        /// <summary>
        /// Represents a class with a read-only field for accessing an instance of an account service.
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initialize the <see cref="CLAccountController"/> private fields and properties.
        /// </summary>
        /// <param name="accountService">The service responsible for managing account-related operations.</param>
        public CLAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Login api for user.
        /// </summary>
        /// <returns>Ok if user successfully login.</returns>
        [HttpGet("Login")]
        public ActionResult Login()
        {
            return Ok("Successfully Login.");
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="objDtoUser">New user information</param>
        /// <returns>Ok response if user register else BadRequest response.</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public ActionResult Register(DtoUSR01 objDtoUser)
        {
            if (_accountService.RegisterUser(objDtoUser))
            {
                return Ok("User register successfully.");
            }

            return BadRequest();
        }
    }
}
