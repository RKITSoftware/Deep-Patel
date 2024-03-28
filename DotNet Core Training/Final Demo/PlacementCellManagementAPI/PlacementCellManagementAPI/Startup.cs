using NLog;
using PlacementCellManagementAPI.Business_Logic;
using PlacementCellManagementAPI.Filters;
using PlacementCellManagementAPI.Interface;
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
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IExceptionLogger, BLException>();
            services.AddScoped<AuthenticationMiddleware>();
            services.AddScoped<IAdminService, BLAdmin>();
            services.AddScoped<IUserService, BLUser>();
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

            app.UseHttpsRedirection();

            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
