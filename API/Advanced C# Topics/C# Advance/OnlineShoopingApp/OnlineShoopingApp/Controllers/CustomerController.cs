using Newtonsoft.Json;
using OnlineShoopingApp.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShoopingApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Customer
        public ActionResult Index() => View();

        [HttpPost]
        public async Task<ActionResult> ViewCart(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/cart/GetCartInfo/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var lstCartItem = JsonConvert.DeserializeObject<List<CartViewModel>>(data);

            return View(lstCartItem);
        }

        [HttpGet]
        public async Task<ActionResult> BuyItem(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/cart/BuyItem/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Success", "Customer");
        }

        [HttpGet]
        public ActionResult Success() => View();

        [HttpGet]
        public ActionResult Delete() => View();

        [HttpGet]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"http://localhost:59592/api/cart/DeleteItem/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Delete", "Customer");
        }

        [HttpGet]
        public async Task<ActionResult> BuyAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/cart/GenerateOtp");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Success", "Customer");
        }

        [HttpGet]
        public async Task<ActionResult> BuyItems(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/cart/GenerateOtp?customerId={id}");
            await _httpClient.SendAsync(request);

            BuyItemsViewModel buyItemsViewModel = new BuyItemsViewModel()
            {
                Id = id
            };

            return View(buyItemsViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> VerifyOTP(BuyItemsViewModel buyItemsViewModel)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"http://localhost:59592/api/cart/VerifyOTP/{buyItemsViewModel.Id}" +
                $"?otp={buyItemsViewModel.Otp}");
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return View("Success");
        }
    }
}