using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IExceptionLogger _exceptionLogger;

        public ExceptionFilter(IExceptionLogger exceptionLogger)
        {
            _exceptionLogger = exceptionLogger;
        }

        public void OnException(ExceptionContext context)
        {
            _exceptionLogger.Log(context.Exception);

            context.Result = new ObjectResult("An error occured.")
            {
                StatusCode = 500
            };
        }
    }
}
