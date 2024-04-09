using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Filter
{
    /// <summary>
    /// Action filter attribute for validating model state before executing an action in Web API.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called before the action method is executed, performs model state validation.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
