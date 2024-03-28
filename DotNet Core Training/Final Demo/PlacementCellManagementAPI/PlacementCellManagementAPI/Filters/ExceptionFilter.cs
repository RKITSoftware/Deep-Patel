using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Filters
{
    /// <summary>
    /// Represents a filter that handles exceptions occurring during HTTP request processing.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Instance of <see cref="IExceptionLogger"/> for logging excepions.
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter"/> class.
        /// </summary>
        /// <param name="exceptionLogger">The logger used to log exceptions.</param>
        public ExceptionFilter(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        /// <summary>
        /// Called when an exception occurs during the processing of an HTTP request.
        /// </summary>
        /// <param name="context">The exception context containing information about the exception.</param>
        public void OnException(ExceptionContext context)
        {
            // Log the exception using the provided logger.
            _exceptionLogger.Log(context.Exception);

            // Set the result to return an error message with status code 500 (Internal Server Error).
            context.Result = new ObjectResult("An error occurred.")
            {
                StatusCode = 500
            };
        }
    }
}
