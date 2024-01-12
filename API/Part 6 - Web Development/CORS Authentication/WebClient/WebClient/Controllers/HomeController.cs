using System.Web.Mvc;

namespace WebClient.Controllers
{
    /// <summary>
    /// Controller class for handling actions related to the home page and other views
    /// </summary>
    public class HomeController : Controller
    {
        #region Public ActionResults

        /// <summary>
        /// Action method for the default index page
        /// </summary>
        /// <returns>Default View</returns>
        public ActionResult Index()
        {
            // Return the default view for the index page
            return View();
        }

        /// <summary>
        /// Action method for the "About" page
        /// </summary>
        /// <returns>About View</returns>
        public ActionResult About()
        {
            // Set a message in the ViewBag for display in the view
            ViewBag.Message = "Your application description page.";

            // Return the view for the "About" page
            return View();
        }

        /// <summary>
        /// Action method for the "Contact" page
        /// </summary>
        /// <returns>Contact View</returns>
        public ActionResult Contact()
        {
            // Set a message in the ViewBag for display in the view
            ViewBag.Message = "Your contact page.";

            // Return the view for the "Contact" page
            return View();
        }

        #endregion
    }
}
