using LoggingUsingNLogAndLoggingProviders.Interface;
using NLog;

namespace LoggingUsingNLogAndLoggingProviders.Services
{
    /// <summary>
    /// Logs the exception to the files using the <see cref="NLog"/> Library.
    /// </summary>
    public class ExceptionLogger : IExceptionLogger
    {
        /// <summary>
        /// Stores the logger information of Project
        /// </summary>
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the exception to the file.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        public void LogException(Exception exception)
        {
            string message = $"\n\t{exception.GetType()}: {exception.Message}\n\t{exception.StackTrace}\n\n";
            _logger.Error(message);
        }
    }
}
