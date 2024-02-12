using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CustomJWTBearerTokenAPI.Filter
{
    public class BearerTokenAuthAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
        }
    }
}