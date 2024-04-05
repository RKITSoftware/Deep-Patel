using NLog;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Business_Logic.Services;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Filters;
using PlacementCellManagementAPI.Middleware;

namespace PlacementCellManagementAPI
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
            LogManager.Setup()
                .LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(configure =>
            {
                configure.Filters.Add(typeof(ExceptionFilter));
                configure.Filters.Add(typeof(ValidateModelFilter));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.JwtConfiguration();
            });

            string developerCorsPolicy = "DevCorsPolicy";
            services.AddCors(options =>
            {
                options.AddPolicy(developerCorsPolicy, builder =>
                {
                    builder.WithOrigins("https://www.google.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetPreflightMaxAge(TimeSpan.FromHours(3));
                });
            });

            services.AddSingleton<IExceptionLogger, BLException>();
            services.AddScoped<AuthenticationMiddleware>();

            services.AddInterfaceServices();
            services.AddJwtAuthentication(Configuration);
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="environment">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            {
                // Enable Swagger and Swagger UI in development environment.
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
                app.UseCors("DevCorsPolicy");
            }
            else
            {
                app.ConfigureExceptionHandler();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure =>
            {
                configure.MapControllers();
            });
        }
    }
}
