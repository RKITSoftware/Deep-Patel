using ServiceStack.OrmLite;
using System.Configuration;
using System.Web.Http;

namespace ORMToolDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            Application["DbFactory"] = dbFactory;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
