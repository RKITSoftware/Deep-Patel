using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace SeriLogDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Creating a startup object
            Startup startup = new Startup(builder.Configuration);

            // Configuring Services
            startup.ConfigureServices(builder.Services);

            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Building App and Starting.
            WebApplication app = builder.Build();
            startup.Configure(app, builder.Environment);
        }
    }
}