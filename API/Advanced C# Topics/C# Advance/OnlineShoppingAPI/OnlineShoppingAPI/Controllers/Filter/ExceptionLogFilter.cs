using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Filter
{
    /// <summary>
    /// My exception filter for writing all exception occured during when server is run.
    /// </summary>
    public class ExceptionLogFilter : ExceptionFilterAttribute
    {
        #region Private Fields

        /// <summary>
        /// Stores the file path where log information of exception want to store.
        /// </summary>
        private readonly string _logFolderPath;

        #endregion Private Fields

        #region Constructor

        public ExceptionLogFilter()
        {
            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Method run when exception occurs
        /// </summary>
        /// <param name="actionExecutedContext">Contains the information about exceptions.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogException(actionExecutedContext.Exception);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Internal Server Error.")
            };
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Writes exception information to a text file.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        private void LogException(Exception exception)
        {
            string filePath = Path.Combine(_logFolderPath, $"{DateTime.Today:dd-MM-yy}.txt");

            // Checks the log file exists or not.
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            string line = Environment.NewLine;
            string _errorMsg = exception.GetType().Name;
            string _exType = exception.GetType().ToString();

            using (StreamWriter writer = File.AppendText(filePath))
            {
                // Error message creation
                string error = $"Time: {DateTime.Now:HH:mm:ss}{line}" +
                               $"Error Message: {_errorMsg}{line}" +
                               $"Exception Type: {_exType}{line}" +
                               $"Error Stack Trace: {exception.StackTrace}{line}";

                writer.WriteLine(error);
                writer.Flush();
            }
        }

        #endregion Private Methods
    }
}