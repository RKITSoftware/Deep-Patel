using OnlineShoppingAPI.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Attribute
{
    public class ValidationIdAttribute : ActionFilterAttribute
    {
        public EnmOperation Operation { get; }

        public ValidationIdAttribute(EnmOperation operation)
        {
            Operation = operation;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var dto = actionContext.ActionArguments.Values.FirstOrDefault();

            int id = (int)dto.GetType().GetProperties()[0].GetValue(dto);

            if (Operation == EnmOperation.A)
            {
                if (id != 0)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    return;
                }
            }

            if (Operation == EnmOperation.E)
            {
                if (id == 0)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    return;
                }
            }
        }
    }
}