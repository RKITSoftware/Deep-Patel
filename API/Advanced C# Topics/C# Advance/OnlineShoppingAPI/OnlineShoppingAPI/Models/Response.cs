﻿using System.Data;
using System.Net;

namespace OnlineShoppingAPI.Models
{
    public class Response
    {
        public bool IsError { get; set; } = false;
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public DataTable Data { get; set; }
    }
}