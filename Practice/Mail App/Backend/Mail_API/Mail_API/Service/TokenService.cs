using Mail_API.Interface;
using Mail_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mail_API.Service
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string GetToken(User user)
        {
            string issuer = Configuration["Jwt:Issuer"];
            string key = Configuration["Jwt:Key"];

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> lstClaim = new()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            };

            JwtSecurityToken secretToken = new(issuer,
                issuer,
                lstClaim,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(secretToken);
        }
    }
}