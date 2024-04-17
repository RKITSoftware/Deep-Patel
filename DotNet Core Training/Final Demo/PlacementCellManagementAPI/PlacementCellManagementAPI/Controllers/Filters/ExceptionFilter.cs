using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models;
using System.Net;

namespace PlacementCellManagementAPI.Controllers.Filters
{
    /// <summary>
    /// Represents a filter that handles exceptions occurring during HTTP request processing.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        #region Private Fields

        /// <summary>
        /// Instance of <see cref="ILoggerService"/> for logging excepions.
        /// </summary>
        private readonly ILoggerService _exceptionLogger;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter"/> class.
        /// </summary>
        /// <param name="exceptionLogger">The logger used to log exceptions.</param>
        public ExceptionFilter(ILoggerService exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Called when an exception occurs during the processing of an HTTP request.
        /// </summary>
        /// <param name="context">The exception context containing information about the exception.</param>
        public void OnException(ExceptionContext context)
        {
            // Log the exception using the provided logger.
            _exceptionLogger.Error(context.Exception);

            // Set the result to return an error message with status code 500 (Internal Server Error).
            context.Result = new ObjectResult(new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Internal Server Error."
            });
        }

        #endregion Public Methods
    }
}