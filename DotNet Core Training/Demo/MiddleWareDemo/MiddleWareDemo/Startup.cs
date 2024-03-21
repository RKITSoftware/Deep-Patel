﻿using MiddleWareDemo.Middleware;

namespace MiddleWareDemo
{
    /// <summary>
    /// Startup class for configuring middleware and services.
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
            services.AddControllers();

            // Endpoint Service
            services.AddEndpointsApiExplorer();

            // Swagger Service
            services.AddSwaggerGen();

            // Register CustomHeaderMiddleware as a singleton service.
            services.AddSingleton<CustomHeaderMiddleware>();
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

            // Enable authorization.
            app.UseAuthorization();

            // Map controllers.
            app.MapControllers();

            // Use CustomHeaderMiddleware to add custom header to responses.
            app.UseMiddleware<CustomHeaderMiddleware>();

            // End the pipeline.
            app.Run();
        }
    }
}
