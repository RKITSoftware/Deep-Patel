using OnlineShoppingAPI.BL.Common;
using System;

namespace OnlineShoppingAPI.Extension
{
    /// <summary>
    /// Extension methods for logging exceptions.
    /// </summary>
    public static class ExceptionLogExtension
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        public static void LogException(this Exception exception)
        {
            BLHelper.LogError(exception);
        }
    }
}
