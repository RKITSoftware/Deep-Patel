namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Represents an interface for logging exceptions.
    /// </summary>
    public interface IExceptionLoggerService
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="exception">The message to be logged.</param>
        void Log(string message);

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        void Log(Exception exception);
    }
}