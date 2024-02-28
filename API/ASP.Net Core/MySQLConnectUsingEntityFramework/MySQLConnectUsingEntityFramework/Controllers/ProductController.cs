using Microsoft.AspNetCore.Mvc;
using MySQLConnectUsingEntityFramework.Data;
using MySQLConnectUsingEntityFramework.Models;

namespace MySQLConnectUsingEntityFramework.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new UserData()
                {
                    Username = userData.Username,
                    Email = userData.Email,
                    Password = userData.Password
                });
                _context.SaveChanges();

                return Ok("User Added successfully.");
            }

            return View();
        }
    }
}
