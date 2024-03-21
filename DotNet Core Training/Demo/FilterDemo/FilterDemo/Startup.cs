using FilterDemo.Filters;
using FilterDemo.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;

namespace FilterDemo
{
    /// <summary>
    /// Startup class responsible for configuring middleware and services.
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the Startup class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Controller Service
            services.AddControllers(options =>
            {
                // Adds a global action filter.
                options.Filters.Add(new MySampleActionFilterAttribute("Global"));
            });

            // Endpoint Service
            services.AddEndpointsApiExplorer();

            // Registers an exception filter globally.
            services.AddSingleton<IExceptionFilter, LoggingExceptionFilter>();

            // Swagger Service
            services.AddSwaggerGen(c =>
            {
                // Adds basic authentication security definition to Swagger.
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header"
                });

                // Adds security requirement for basic authentication to Swagger.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

                        new string[] {}
                    }
                });
            });

            // Adds basic authentication scheme to the application.
            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                // Enable Swagger and Swagger UI in development environment.
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Enable authentication.
            app.UseAuthentication();

            // Enable authorization.
            app.UseAuthorization();

            // Map controllers.
            app.MapControllers();

            // End the pipeline.
            app.Run();
        }
    }
}
