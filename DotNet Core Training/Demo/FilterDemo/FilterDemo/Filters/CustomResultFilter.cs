using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// Result filter for handling response after an action successfully processed.
    /// </summary>
    public class CustomResultFilterAttribute : Attribute, IResultFilter
    {
        /// <summary>
        /// Called before the response is send back to the user.
        /// </summary>
        /// <param name="context">The result executing context.</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Executing result filter of :- " + context.ActionDescriptor.DisplayName);
        }

        /// <summary>
        /// Called after the response if send back to the user.
        /// </summary>
        /// <param name="context">The result executed context.</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Executed result filter of :- " + context.ActionDescriptor.DisplayName);
        }
    }
}
