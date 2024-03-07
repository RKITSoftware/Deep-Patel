using OnlineShoopingApp.Helpers;
using OnlineShoopingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoopingApp.Controllers
{
    public class AccountController : Controller
    {
        private Uri baseAddress = new Uri("http://localhost:59592/");
        private readonly HttpClient _httpClient;

        public AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Login" +
                    $"?username={userLoginViewModel.Username}&password={userLoginViewModel.Password}");

                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<string> cookieHeaders;
                    if (response.Headers.TryGetValues("Set-Cookie", out cookieHeaders))
                    {
                        foreach (var cookieHeader in cookieHeaders)
                        {
                            var cookies = Helper.SetCookiesFromHeader(cookieHeader);

                            foreach (var cookie in cookies)
                            {
                                Response.Cookies.Add(new HttpCookie(cookie.Name, cookie.Value));
                            }
                        }
                    }
                    return RedirectToAction("Index", "Account");
                }
            }

            return View(userLoginViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Logout");

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<string> cookieHeaders;
                if (response.Headers.TryGetValues("Set-Cookie", out cookieHeaders))
                {
                    foreach (var cookieHeader in cookieHeaders)
                    {
                        var cookies = Helper.SetCookiesFromHeader(cookieHeader);

                        foreach (var cookie in cookies)
                        {
                            Response.Cookies.Add(new HttpCookie(cookie.Name, cookie.Value));
                        }
                    }
                }

                return RedirectToAction("LogIn", "Account");
            }

            return RedirectToAction("Index", "Account");
        }
    }
}