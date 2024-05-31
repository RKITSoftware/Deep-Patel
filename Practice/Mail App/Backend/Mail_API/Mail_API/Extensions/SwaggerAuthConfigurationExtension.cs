using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mail_API.Extensions
{
    public static class SwaggerAuthConfigurationExtension
    {
        public static void JwtConfiguration(this SwaggerGenOptions options)
        {
            // Add security definition for JWT token-based authentication
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Jwt Token Based Authentication",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            // Add security requirement for JWT authentication
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }
}
