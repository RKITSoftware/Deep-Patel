﻿using NLog;
using PlacementCellManagementAPI.Business_Logic.Interface;

namespace PlacementCellManagementAPI.Business_Logic.Services
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
                $"\n\t{exception.GetType()}: {exception.Message}\n\t{exception.StackTrace}\n";
            _logger.Error(message);
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="exception">The message to be logged.</param>
        public void Log(string message)
        {
            _logger.Error(message + Environment.NewLine);
        }
    }
}