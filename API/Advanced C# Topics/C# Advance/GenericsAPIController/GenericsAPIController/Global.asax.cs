using GenericsAPIController.Business_Logic;
using System.Web.Http;

namespace GenericsAPIController
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var blEmployee = new BLEmployee();
            GlobalConfiguration.Configuration.DependencyResolver = new MyDependencyResolver(blEmployee);
        }
    }
}
