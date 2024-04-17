namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Represents an interface for logging exceptions.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Logs the error message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void Error(string message);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="exception">Exception to log.</param>
        void Error(Exception exception);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void Information(string message);

        /// <summary>
        /// Logs the warning message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        void Warning(string message);
    }
}