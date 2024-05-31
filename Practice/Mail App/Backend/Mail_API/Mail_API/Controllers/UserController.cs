using Mail_API.Dtos;
using Mail_API.Extensions;
using Mail_API.Interface;
using Mail_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mail_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto is null)
                throw new ArgumentNullException(nameof(registerUserDto));

            User user = registerUserDto.Convert<User>();

            return Ok(_userRepository.RegisterUser(user));
        }

        [HttpPost("login")]
        public IActionResult LoginUser(LoginUserDto loginUserDto)
        {
            if (loginUserDto is null)
                throw new ArgumentNullException(nameof(loginUserDto));

            return Ok(_userRepository.LoginUser(loginUserDto));
        }
    }
}
