namespace FilterDemo
{
    public class Program
    {
        /// <summary>
        /// The entry point method for the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            // Create a web application builder.
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Create a startup object.
            Startup startup = new Startup(builder.Configuration);

            // Configure services using the startup object.
            startup.ConfigureServices(builder.Services);

            // Build the application.
            WebApplication app = builder.Build();

            // Configure the application using the startup object and environment.
            startup.Configure(app, builder.Environment);
        }
    }
}
