namespace PlacementCellManagementAPI.Business_Logic.Common.Interface
{
    /// <summary>
    /// Interface for user-specific logging services.
    /// </summary>
    public interface IUserLoggerService
    {
        #region Public Methods

        /// <summary>
        /// Logs the specified message with the Info level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void Log(string message);

        #endregion
    }
}
