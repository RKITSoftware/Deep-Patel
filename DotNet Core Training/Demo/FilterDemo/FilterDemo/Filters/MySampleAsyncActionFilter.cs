using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// A sample asynchronous action filter attribute.
    /// </summary>
    public class MySampleAsyncActionFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the MySampleAsyncActionFilterAttribute class.
        /// </summary>
        /// <param name="name">The name associated with the filter.</param>
        public MySampleAsyncActionFilterAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Called asynchronously before the action method executes.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        /// <param name="next">The delegate representing the next action filter or the action itself.</param>
        /// <returns>A task that represents the asynchronous on action execution operation.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Perform actions before the execution of the action method.
            Console.WriteLine("Before Async Execution. " + _name);

            // Call the next action filter or the action method itself.
            await next();

            // Perform actions after the execution of the action method.
            Console.WriteLine("After Async Execution. " + _name);
        }
    }
}
