using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTokenDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LogInController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post()
        {
            string username = "Deep";
            string password = "123";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> lstClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.PostalCode, password)
            };

            var secretToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                lstClaims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(secretToken);

            return Ok(token);
        }
    }
}
