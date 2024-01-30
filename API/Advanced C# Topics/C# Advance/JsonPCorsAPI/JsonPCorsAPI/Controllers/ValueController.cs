using System.Web.Http;

namespace JsonPCorsAPI.Controllers
{
    public class ValueController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Hello Deep");
        }

        /*
            function fetchJsonp(url, callbackName) {
              // Create a Promise that will be resolved when the script is loaded
              return new Promise((resolve, reject) => {
                // Generate a unique callback function name
                const callbackFuncName = `jsonpCallback_${Math.round(100000 * Math.random())}`;

                // Attach the callback function to the window object
                window[callbackFuncName] = function(data) {
                  // Clean up: remove the script tag and the callback function from the window
                  document.body.removeChild(script);
                  delete window[callbackFuncName];

                  // Resolve the Promise with the received data
                  resolve(data);
                };

                // Set up the script tag with the JSONP URL
                const script = document.createElement('script');
                script.src = `${url}?callback=${callbackFuncName}`;

                // Attach an error event listener to the script
                script.onerror = function() {
                  // Clean up: remove the script tag and the callback function from the window
                  document.body.removeChild(script);
                  delete window[callbackFuncName];

                  // Reject the Promise with an error
                  reject(new Error('Error loading script'));
                };

                // Append the script tag to the document body
                document.body.appendChild(script);
              });
            }

            // Example usage
            const jsonpUrl = 'https://localhost:44303/api/Value';
            fetchJsonp(jsonpUrl)
              .then(data => {
                console.log('JSONP-like data:', data);
              })
              .catch(error => {
                console.error('Error fetching JSONP-like data:', error);
              });
        */
    }
}
