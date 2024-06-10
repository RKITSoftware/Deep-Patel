using NLog;
using PlacementCellManagementAPI.Business_Logic.Common.Interface;
using System.Security.Claims;

namespace PlacementCellManagementAPI.Business_Logic.Common.Service
{
    /// <summary>
    /// Service for logging user-specific information.
    /// </summary>
    public class UserLoggerService : IUserLoggerService
    {
        #region Private Fields

        /// <summary>
        /// Instance for logging the user related information.
        /// </summary>
        private readonly Logger _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserLoggerService"/> class.
        /// Configures the logger to log user-specific information based on the user's ID.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor to the current HTTP context, used to retrieve user information.</param>
        public UserLoggerService(IHttpContextAccessor httpContextAccessor)
        {
            // Retrieve the user's ID from the HTTP context.
            string id = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Define the folder path for the log file based on the current directory and user ID.
            string logFolderPath = Directory.GetCurrentDirectory();
            LogManager.Configuration.Variables["logFilePath2"] = Path.Combine(logFolderPath, "logs", id, DateTime.Now.ToString("dd-MM-yyyy") + ".txt");

            // Reconfigure the loggers to apply the new file path.
            LogManager.ReconfigExistingLoggers();

            // Initialize the logger with the specified logger name.
            _logger = LogManager.GetLogger("logger2");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Logs the specified message with the Info level.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            _logger.Info(message);
        }

        #endregion
    }
}
