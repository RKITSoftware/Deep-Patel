namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Represents an interface for logging exceptions.
    /// </summary>
    public interface IExceptionLogger
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be logged.</param>
        void Log(Exception exception);
    }
}