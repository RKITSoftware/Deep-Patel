using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SchoolManagementAPI.Filters
{
    /// <summary>
    /// Custom authorization attribute for Web API actions.
    /// </summary>
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        #region Override Methods

        /// <summary>
        /// Handles the response when the request is unauthorized.
        /// </summary>
        /// <param name="actionContext">The context for the action being executed.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Check if the user is authenticated.
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // If the user is authenticated, let the base class handle the unauthorized request.
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // If the user is not authenticated, return an unauthorized response.
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        #endregion
    }
}
