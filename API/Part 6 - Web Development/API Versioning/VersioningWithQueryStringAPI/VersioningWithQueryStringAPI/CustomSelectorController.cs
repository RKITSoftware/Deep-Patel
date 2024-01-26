using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace VersioningWithQueryStringAPI
{
    // Custom controller selector that extends the default HTTP controller selector
    [RoutePrefix("api/{controller}")]
    public class CustomSelectorController : DefaultHttpControllerSelector
    {
        #region Private Fields

        /// <summary>
        /// Reference to the HTTP configuration
        /// </summary>
        private HttpConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the custom controller selector with the configuration
        /// </summary>
        /// <param name="config"></param>
        public CustomSelectorController(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// Override the method to select the appropriate controller based on versioning
        /// </summary>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // Get a mapping of all possible API controllers
            var controllers = GetControllerMapping();

            // Get route data from the request
            var routeData = request.GetRouteData();

            // Get the controller name from the route data
            var controllerName = routeData.Values["controller"].ToString();

            // Set a default API version
            string apiVersion = "1";

            /// Accept Header Code
            // Extract version information from the "Accept" header
            var acceptHeader = request.Headers.Accept
                .Where(b => b.Parameters.Count(t => t.Name.ToLower() == "version") > 0);
            if (acceptHeader.Any())
            {
                apiVersion = acceptHeader.First().Parameters.First(x => x.Name.ToLower() == "version").Value;
            }

            // Get version information from the query string in the URI
            //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //if (versionQueryString["version"] != null)
            //{
            //    apiVersion = Convert.ToString(versionQueryString["version"]);
            //}

            // Append the version to the controller name
            if (apiVersion == "1")
            {
                controllerName = controllerName + "V1";
            }
            else
            {
                controllerName = controllerName + "V2";
            }

            HttpControllerDescriptor controllerDescriptor;

            // Check if the controller name exists in the controllers dictionary
            // TryGetValue is an efficient way to check the value existence
            if (controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                // Return the selected controller descriptor
                return controllerDescriptor;
            }

            // Return null if the controller is not found
            return null;
        }

        #endregion
    }
}
