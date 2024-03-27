namespace PlacementCellManagementAPI
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

            // Building App and Starting.
            WebApplication app = builder.Build();
            startup.Configure(app, builder.Environment);
        }
    }
}