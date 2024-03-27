using BookMyShowAPI.Business_Logic;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Middleware;
using BookMyShowAPI.Services;

namespace BookMyShowAPI.Extensions
{
    /// <summary>
    /// Project's Extension Methods container
    /// </summary>
    public static class BMSExtensions
    {
        /// <summary>
        /// Adding services which is used in controller.
        /// </summary>
        /// <param name="services">Services of projects</param>
        /// <returns><see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, BLAccount>();
            services.AddScoped<ITheatreService, BLTheatre>();
            services.AddScoped<IMovieService, BLMovie>();

            return services;
        }

        /// <summary>
        /// Adding database services for the project.
        /// </summary>
        /// <param name="services">Services of projects</param>
        /// <returns><see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddDatabaseService(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            return services;
        }

        /// <summary>
        /// Adding middleware services for the project.
        /// </summary>
        /// <param name="services">Services of projects</param>
        /// <returns><see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationMiddleware>();
            return services;
        }
    }
}
