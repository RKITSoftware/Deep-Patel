using MyLibrary.Business_Logic;
using System.Web.Http.Filters;

namespace MyLibrary.Filter
{
    /// <summary>
    /// My exception filter for writing all exception occured during when server is run.
    /// </summary>
    public class ExceptionLogFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Path to Log folder of web api
        /// </summary>
        private readonly string _path;

        public ExceptionLogFilter(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Method run when exception occurs
        /// </summary>
        /// <param name="actionExecutedContext">Contains the information about exceptions.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ExceptionLogging.SendErrorToTxt(actionExecutedContext.Exception, _path);
        }
    }
}
