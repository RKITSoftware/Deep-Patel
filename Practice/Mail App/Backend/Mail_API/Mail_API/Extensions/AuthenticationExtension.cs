using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Mail_API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            string issuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            string key = configuration.GetSection("Jwt:Key").Get<string>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }
    }
}
