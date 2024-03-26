using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// For Establishing the database connectivity to the specified server.
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// Called after the authentication process finished.
        /// </summary>
        /// <param name="context">The resource executing context.</param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("Established database connection");
        }

        /// <summary>
        /// Called after the action filter and result is send back to the user for releasing the database connections.
        /// </summary>
        /// <param name="context">The resource executed context.</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Released database connection");
        }
    }
}
