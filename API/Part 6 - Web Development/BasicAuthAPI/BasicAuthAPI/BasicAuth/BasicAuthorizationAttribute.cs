using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BasicAuthAPI.BasicAuth
{
    /// <summary>
    /// Custom Authorization Attribute for Basic Authentication
    /// </summary>
    public class BasicAuthorizationAttribute : AuthorizeAttribute
    {
        #region Override Methods

        /// <summary>
        /// Override the method to handle unauthorized requests
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Check if the user is authenticated
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // If the user is authenticated, proceed with the base implementation for unauthorized requests
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // If the user is not authenticated, set the response to Forbidden (403)
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        #endregion
    }
}
