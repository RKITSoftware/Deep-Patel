using System.Web;
using System.Web.Http;

namespace SchoolManagementAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["LogFolderPath"] = HttpContext.Current.Server.MapPath("~/Logs");
            Application["AdminData"] = HttpContext.Current.Server.MapPath("~/Data/Admin Data.json");
            Application["StudentData"] = HttpContext.Current.Server.MapPath("~/Data/Student Data.json");
            Application["UserData"] = HttpContext.Current.Server.MapPath("~/Data/User Data.json");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
