using ServiceStack.OrmLite;
using System.Configuration;
using System.Web;
using System.Web.Http;

namespace OnlineShoppingAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database connection using connection string and orm lite tool.
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connectionString, MySqlDialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = dbFactory;
            Application["LogFolderPath"] = HttpContext.Current.Server.MapPath("~/Logs");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
