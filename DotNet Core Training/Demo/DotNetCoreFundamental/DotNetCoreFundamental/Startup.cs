namespace DotNetCoreFundamental
{
    /// <summary>
    /// Startup class for configuring services and web API.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Public constructor to initialize the configuration property for further database connectivity
        /// and getting values from the appSettings.json file.
        /// </summary>
        /// <param name="configuration">Stores the appsettings.json file data</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Stores the appsettings.json file data and gets that data when needed.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for the project.
        /// </summary>
        /// <param name="services">Web Application's Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adds MVC services to the services container.
            services.AddControllersWithViews();
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application's request processing pipeline.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Shows developer-friendly exception page.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Redirects to the error page in production.
                app.UseExceptionHandler("/Error");

                // Adds HTTP Strict Transport Security (HSTS) middleware to the pipeline.
                app.UseHsts();
            }

            // Redirects HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Serves static files (wwwroot folder).
            app.UseStaticFiles();

            // Enables routing.
            app.UseRouting();

            // Adds authorization middleware to the pipeline.
            app.UseAuthorization();

            // Configures endpoint routing for controllers.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
