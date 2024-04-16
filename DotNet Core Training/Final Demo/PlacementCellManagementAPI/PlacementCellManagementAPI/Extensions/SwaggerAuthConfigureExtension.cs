using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PlacementCellManagementAPI.Extensions
{
    /// <summary>
    /// Extension methods for configuring Swagger authentication.
    /// </summary>
    public static class SwaggerAuthConfigureExtension
    {
        #region Public Methods

        /// <summary>
        /// Configures basic authentication for Swagger.
        /// </summary>
        /// <param name="options">The Swagger generation options to configure.</param>
        public static void BasicAuthConfuration(this SwaggerGenOptions options)
        {
            // Adds basic authentication security definition to Swagger.
            options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header"
            });

            // Adds security requirement for basic authentication to Swagger.
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }

        /// <summary>
        /// Configures JWT authentication for Swagger.
        /// </summary>
        /// <param name="options">The Swagger generation options to configure.</param>
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

        #endregion Public Methods
    }
}