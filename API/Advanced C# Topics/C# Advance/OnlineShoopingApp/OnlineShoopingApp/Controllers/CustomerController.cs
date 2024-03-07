using System;
using System.Net.Http;
using System.Web.Mvc;

namespace OnlineShoopingApp.Controllers
{
    public class CustomerController : Controller
    {
        private Uri _baseAddress = new Uri("http://localhost:59592/api/CLAdmin");
        private readonly HttpClient _httpClient;

        public CustomerController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseAddress;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}