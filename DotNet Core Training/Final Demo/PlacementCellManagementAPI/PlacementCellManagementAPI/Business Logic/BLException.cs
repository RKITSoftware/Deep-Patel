using NLog;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Provides functionality to log exceptions using NLog.
    /// </summary>
    public class BLException : IExceptionLogger
    {
        /// <summary>
        /// Gets the current class logger information.
        /// </summary>
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        public void Log(Exception exception)
        {
            string message =
                $"\n\t{exception.GetType()}: {exception.Message}\n\t{exception.StackTrace}\n\n";
            _logger.Error(message);
        }
    }
}
