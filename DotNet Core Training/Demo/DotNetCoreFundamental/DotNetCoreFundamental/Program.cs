namespace DotNetCoreFundamental
{
    public class Program
    {
        /// <summary>
        /// Starting point of web api to configure and use services and middlwares.
        /// </summary>
        /// <param name="args">Passed when running from terminal.</param>
        public static void Main(string[] args)
        {
            // Building and Starting app Using Startup class.
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creating app using Startup class.
        /// </summary>
        /// <param name="args">Passed when running from terminal.</param>
        /// <returns>Creating instance of WebApplication</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}