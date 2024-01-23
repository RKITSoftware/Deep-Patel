using ServiceStack.OrmLite;
using System.Configuration;
using System.Web.Http;

namespace ORMToolDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database connection using connection string and orm lite tool.
            var connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = dbFactory;

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
