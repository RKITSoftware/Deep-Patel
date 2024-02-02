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
        private readonly string Path;

        public ExceptionLogFilter(string Path)
        {
            this.Path = Path;
        }

        /// <summary>
        /// Method run when exception occurs
        /// </summary>
        /// <param name="actionExecutedContext">Contains the information about exceptions.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            ExceptionLogging.SendErrorToTxt(actionExecutedContext.Exception, this.Path);
        }
    }
}
