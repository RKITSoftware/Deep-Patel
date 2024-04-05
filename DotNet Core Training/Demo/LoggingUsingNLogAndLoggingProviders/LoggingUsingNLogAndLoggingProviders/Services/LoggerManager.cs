using LoggingUsingNLogAndLoggingProviders.Interface;
using NLog;

namespace LoggingUsingNLogAndLoggingProviders.Services
{
    /// <summary>
    /// Logs the information according to the type.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        /// <summary>
        /// Stores the logger information of current project.
        /// </summary>
        private static NLog.ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the debug information
        /// </summary>
        /// <param name="message">Debug message</param>
        public void LogDebug(string message) => _logger.Debug(message);

        /// <summary>
        /// Logs the Error message
        /// </summary>
        /// <param name="message">Error message</param>
        public void LogError(string message) => _logger.Error(message);

        /// <summary>
        /// Logs the Information
        /// </summary>
        /// <param name="message">Information Message</param>
        public void LogInfo(string message) => _logger.Info(message);

        /// <summary>
        /// Logs the Warning
        /// </summary>
        /// <param name="message">Warning Message</param>
        public void LogWarn(string message) => _logger.Warn(message);
    }
}
