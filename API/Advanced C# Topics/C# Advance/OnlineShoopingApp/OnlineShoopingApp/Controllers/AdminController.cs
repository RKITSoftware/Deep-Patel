using Newtonsoft.Json;
using OnlineShoopingApp.Models;
using OnlineShoopingApp.ViewModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineShoopingApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminController()
        {
            _httpClient = new HttpClient();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ViewCustomer()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://localhost:59592/api/CLCustomer/GetCustomers");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var lstCustomer = JsonConvert.DeserializeObject<List<Customer>>(data);

            return View(lstCustomer);
        }

        [HttpGet]
        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer(Customer customer)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                "http://localhost:59592/api/CLCustomer/CreateCustomer");
            string data = JsonConvert.SerializeObject(customer);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return RedirectToAction("ViewCustomer", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> EditCustomer(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/CLCustomer/GetCustomerById/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(data);

            return View(customer);
        }

        [HttpPost]
        public async Task<ActionResult> EditCustomer(Customer customer)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                "http://localhost:59592/api/CLCustomer/UpdateCustomer");
            string data = JsonConvert.SerializeObject(customer);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("ViewCustomer", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"http://localhost:59592/api/CLCustomer/DeleteCustomer/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ViewCustomer", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> ViewSupliers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
               "http://localhost:59592/api/CLSuplier/GetSupliers");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var lstSuplier = JsonConvert.DeserializeObject<List<Suplier>>(data);

            return View(lstSuplier);
        }

        [HttpGet]
        public ActionResult CreateSuplier()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateSuplier(Suplier suplier)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                "http://localhost:59592/api/CLSuplier/Create");
            string data = JsonConvert.SerializeObject(suplier);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return RedirectToAction("ViewSupliers", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSuplier(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"http://localhost:59592/api/CLSuplier/DeleteSuplier/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ViewSupliers", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> EditSuplier(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/CLSuplier/GetSuplier/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var suplier = JsonConvert.DeserializeObject<Suplier>(data);

            return View(suplier);
        }

        [HttpPost]
        public async Task<ActionResult> EditSuplier(Suplier suplier)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                "http://localhost:59592/api/CLSuplier/UpdateSuplier");
            string data = JsonConvert.SerializeObject(suplier);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("ViewSupliers", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> ViewCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
               "http://localhost:59592/api/CLCategory/GetAll");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var lstCategory = JsonConvert.DeserializeObject<List<Category>>(data);

            return View(lstCategory);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                "http://localhost:59592/api/CLCategory/Add");
            string data = JsonConvert.SerializeObject(category);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);

            return RedirectToAction("ViewCategories", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete,
                $"http://localhost:59592/api/CLCategory/Delete/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ViewCategories", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://localhost:59592/api/CLCategory/Get/{id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(data);

            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(Category category)
        {
            var request = new HttpRequestMessage(HttpMethod.Put,
                "http://localhost:59592/api/CLCategory/Edit");
            string data = JsonConvert.SerializeObject(category);
            var content = new StringContent(data, null, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("ViewCategories", "Admin");
        }

        [HttpGet]
        public async Task<ActionResult> ViewProducts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
               "http://localhost:59592/api/CLProduct/GetAllProductsInfo");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();
            var lstProducts = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

            return View(lstProducts);
        }
    }
}