using ServiceStack.OrmLite;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Hosting;
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

            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(
                connectionString, MySqlDialect.Provider);

            // Storing OrmLiteConnectionFactory instance for further usage in any other component.
            Application["DbFactory"] = dbFactory;
            Application["LogFolderPath"] = HttpContext.Current.Server.MapPath("~/Logs");
            Application["Credentials"] = new NetworkCredential(
                ConfigurationManager.AppSettings["Username"],
                ConfigurationManager.AppSettings["Password"]); ;

            string path = HostingEnvironment.MapPath("~/Logs");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
