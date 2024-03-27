using BookMyShowAPI.Interface;
using NLog;

namespace BookMyShowAPI.Services
{
    /// <summary>
    /// Logs the exception which is handle by user when any database calls happens
    /// or any functions that will give exceptions.
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
        /// <param name="exception"><see cref="Exception"/> to log.</param>
        public void Log(Exception exception)
        {
            string message = $"\n\t{exception.GetType()}: {exception.Message}\n\t{exception.StackTrace}\n\n";
            _logger.Error(message);
        }
    }
}
