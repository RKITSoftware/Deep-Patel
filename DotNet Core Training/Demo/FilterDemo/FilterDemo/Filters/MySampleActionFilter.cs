using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterDemo.Filters
{
    /// <summary>
    /// A sample action filter attribute.
    /// </summary>
    public class MySampleActionFilterAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        private readonly string _name;

        /// <summary>
        /// Gets the order in which the filter will be executed.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Initializes a new instance of the MySampleActionFilterAttribute class.
        /// </summary>
        /// <param name="name">The name associated with the filter.</param>
        /// <param name="order">The order in which the filter will be executed (default is 0).</param>
        public MySampleActionFilterAttribute(string name, int order = 0)
        {
            _name = name;
            Order = order;
        }

        /// <summary>
        /// Called before the action method executes.
        /// </summary>
        /// <param name="context">The action executing context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Perform actions before the execution of the action method.
            Console.WriteLine("OnActionExecuting. " + _name);
        }

        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <param name="context">The action executed context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Perform actions after the execution of the action method.
            Console.WriteLine("OnActionExecuted. " + _name);
            Console.WriteLine();
        }
    }
}
