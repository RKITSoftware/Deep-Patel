namespace PlacementCellManagementAPI
{
    /// <summary>
    /// The main entry point for the Placement Cell Management API application.
    /// </summary>
    public class Program
    {
        #region Public Methods

        /// <summary>
        /// The main method, which is the entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates and configures an IHostBuilder instance.
        /// </summary>
        /// <param name="args">An array of command-line arguments.</param>
        /// <returns>An initialized and configured IHostBuilder instance.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        #endregion
    }
}
