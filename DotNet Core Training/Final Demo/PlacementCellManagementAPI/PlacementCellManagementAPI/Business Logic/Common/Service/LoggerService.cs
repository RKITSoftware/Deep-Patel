using NLog;
using PlacementCellManagementAPI.Business_Logic.Interface;

namespace PlacementCellManagementAPI.Business_Logic.Services
{
    /// <summary>
    /// Provides functionality to log exceptions using NLog.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Gets the current class logger information.
        /// </summary>
        private static Logger _logger = LogManager.GetLogger("logger1");

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void Error(string message)
        {
            _logger.Error(message + Environment.NewLine);
        }

        /// <summary>
        /// Logs details of an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public void Error(Exception exception)
        {
            string message =
                $"\n\t{exception.GetType()}: {exception.Message}\n\t{exception.StackTrace}\n";
            _logger.Error(message);
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        public void Information(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void Warning(string message)
        {
            _logger.Warn(message);
        }
    }
}
