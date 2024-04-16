using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlacementCellManagementAPI.Controllers.Filters
{
    /// <summary>
    /// Represents a filter that validates the model state before executing an action.
    /// </summary>
    public class ValidateModelFilter : IAsyncActionFilter
    {
        #region Public Methods

        /// <summary>
        /// Called asynchronously before the action method is invoked, to validate the model state.
        /// </summary>
        /// <param name="context">The context for the action execution.</param>
        /// <param name="next">The delegate representing the remaining middleware in the pipeline.</param>
        /// <returns>A task that represents the asynchronous on action execution operation.</returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Check if the model state is invalid.
            if (!context.ModelState.IsValid)
            {
                // If the model state is invalid, return a BadRequest response containing the model state errors.
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            // If the model state is valid, proceed to the next action.
            await next();
        }

        #endregion Public Methods
    }
}