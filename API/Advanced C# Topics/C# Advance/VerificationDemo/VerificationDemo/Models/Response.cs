using System.Net;

namespace VerificationDemo.Models
{
    /// <summary>
    /// Response for the specific response during api request.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or Sets the response according to the request.
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or Sets the error occur during request.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or Sets the message according to the success or error response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets the status code during request accoring to the success or error.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }
}