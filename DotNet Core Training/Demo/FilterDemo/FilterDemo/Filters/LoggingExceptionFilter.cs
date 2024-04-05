using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Filter for logging exceptions to a file.
    /// </summary>
    public class LoggingExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the LoggingExceptionFilter class.
        /// </summary>
        /// <param name="environment">The hosting environment.</param>
        public LoggingExceptionFilter(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Handles the exception by logging it to a file.
        /// </summary>
        /// <param name="context">The exception context.</param>
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("Exception occured.");

            // Log exception to file
            LogExceptionToFile(context.Exception);
        }

        /// <summary>
        /// Logs the exception details to a file.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        private void LogExceptionToFile(Exception exception)
        {
            // Construct the path for the log file
            string logFilePath = Path.Combine(_environment.ContentRootPath,
                "ExceptionLog", DateTime.Now.ToString("dd-MM-yyyy"));

            // Create the log message including exception details
            string logMessage =
                $"[{DateTime.Now}] {exception.GetType()}: {exception.Message}\n{exception.StackTrace}\n\n";

            // Append the exception details to the log file
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
