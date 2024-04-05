using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspDotNetCoreRequestProcessingPipeline.Filter
{
    public class LogAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Execute before the controller's action method invoked.
        /// </summary>
        /// <param name="context">Http Action context of current request</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Trace.WriteLine(string.Format("Action method {0} excecuting at {1}",
                context.ActionDescriptor.DisplayName, DateTime.Now.ToString()));
        }

        /// <summary>
        /// Execute after the controller's action method finished.
        /// </summary>
        /// <param name="context">Http Action context of current request</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Trace.WriteLine(string.Format("Action method {0} executed at {1}",
                context.ActionDescriptor.DisplayName, DateTime.Now.ToString()));
        }
    }
}
