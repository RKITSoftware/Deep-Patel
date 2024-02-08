using HttpCachingAPI.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.OutputCache.V2;

namespace HttpCachingAPI.Controllers
{
    /// <summary>
    /// Controller for handling CLData-related operations
    /// </summary>
    public class CLDataController : ApiController
    {
        #region Get Endpoints

        /// <summary>
        /// Action method to get data with caching enabled
        /// </summary>
        /// <returns>Dictionary of States with Id</returns>
        [AllowAnonymous]
        [Route("GetData")]
        // [CacheFilter(TimeDuration = 100)] // Apply the custom CacheFilter attribute with a caching duration of 100 seconds
        [CacheOutput(ClientTimeSpan = 10)]
        public async Task<IHttpActionResult> GetData()
        {
            // Sample data to be returned
            Dictionary<string, string> obj = new Dictionary<string, string>
            {
                { "1", "Punjab" },
                { "2", "Assam" },
                { "3", "UP" },
                { "4", "AP" },
                { "5", "J&K" },
                { "6", "Odisha" },
                { "7", "Delhi" },
                { "9", "Karnataka" },
                { "10", "Bangalore" },
                { "21", "Rajesthan" },
                { "31", "Jharkhand" },
                { "41", "Chennai" },
                { "51", "Jammu" },
                { "61", "Bhubaneshwar" },
                { "71", "Delhi" },
                { "19", "Karnataka" }
            };

            // Return the data as an HTTP 200 OK response
            return Ok(obj);
        }

        #endregion
    }
}
