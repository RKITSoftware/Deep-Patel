﻿using System.Net;

namespace AJAXPracticeAPI.Models
{
    public class Response
    {
        public bool IsError { get; set; } = false;

        public string? Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public dynamic? Data { get; set; }
    }
}
