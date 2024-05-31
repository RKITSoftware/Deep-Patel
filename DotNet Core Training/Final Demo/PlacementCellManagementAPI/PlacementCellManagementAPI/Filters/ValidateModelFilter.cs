﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlacementCellManagementAPI.Filters
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
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }

            await next();
        }

        #endregion Public Methods
    }
}