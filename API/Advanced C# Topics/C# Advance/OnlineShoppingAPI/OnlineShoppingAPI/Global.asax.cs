using ServiceStack.OrmLite;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Http;

namespace OnlineShoppingAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Database connection using connection string and orm lite tool.
            string connectionString = ConfigurationManager
                .ConnectionStrings["MyConnectionString"].ConnectionString;
            string backupDBConnectionString = ConfigurationManager
                .ConnectionStrings["BackupDBConnectionString"].ConnectionString;

            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(
                connectionString, MySqlDialect.Provider);

            OrmLiteConnectionFactory backupDbFactory = new OrmLiteConnectionFactory(
                backupDBConnectionString, MySqlDialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = dbFactory;
            Application["BackupDBFactory"] = backupDbFactory;

            Application["LogFolderPath"] = HttpContext.Current.Server.MapPath("~/Logs");
            Application["Credentials"] = new NetworkCredential(
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
