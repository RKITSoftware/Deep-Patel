using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Business_Logic.Services;
using System.Text;

namespace PlacementCellManagementAPI.Extensions
{
    /// <summary>
    /// Extension methods for configuring services in ASP.NET Core.
    /// </summary>
    public static class ConfigureServiceExtension
    {
        /// <summary>
        /// Adds transient services for various interfaces and their corresponding implementations.
        /// </summary>
        /// <param name="services">The collection of services to add to.</param>
        public static void AddInterfaceServices(this IServiceCollection services)
        {
            services.AddTransient<IAdminService, BLAdmin>();
            services.AddTransient<IUserService, BLUser>();

            services.AddTransient<IStudentService, BLStudent>();
            services.AddTransient<ICompanyService, BLCompany>();

            services.AddTransient<IJobService, BLJob>();
            services.AddTransient<ITokenService, BLToken>();
        }

        /// <summary>
        /// Adds JWT authentication to the application.
        /// </summary>
        /// <param name="services">The collection of services to add to.</param>
        /// <param name="configuration">The configuration for JWT authentication.</param>
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
