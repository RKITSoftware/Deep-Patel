using System.Net;

namespace Mail_API.Models
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
        public bool IsError { get; set; } = false;
    }
}
