using System.Configuration;
using System.Web.Http;

namespace VerificationDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["Development"].ConnectionString;

            Application["DevConnectionString"] = connectionString;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
