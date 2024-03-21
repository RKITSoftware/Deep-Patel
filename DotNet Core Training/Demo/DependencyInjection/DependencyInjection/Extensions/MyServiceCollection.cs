using DependencyInjection.Business_Logic;
using DependencyInjection.Interface;
using DependencyInjection.Middleware;

namespace DependencyInjection.Extensions
{
    public static class MyServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Creating services for controller injection.
            services.AddScoped<IEmployeeService, BLEmployee>();

            services.AddSingleton<DateCustomMiddleware>();

            // services.AddTransient<IDateTime, Time>();
            // services.AddScoped<IDateTime, Time>();
            services.AddSingleton<IDateTime, Time>();

            return services;
        }
    }
}
