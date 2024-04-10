using System.Net;

namespace OnlineShoppingAPI.Models
{
    /// <summary>
    /// Represents a response from the API.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Indicates whether the response represents an error.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// A message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// The data associated with the response.
        /// </summary>
        public dynamic Data { get; set; }
    }
}
