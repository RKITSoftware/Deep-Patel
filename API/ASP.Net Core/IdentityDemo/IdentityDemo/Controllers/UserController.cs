using IdentityDemo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _user;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _user = userManager;
        }

        //public IActionResult Index()
        //{
        //    var userList = _db.Users.ToList();
        //    var userRole = _db.UserRoles.ToList();
        //    var roles = _db.Roles.ToList();
        //    //set user to none to not make ui look terrible
        //    foreach (var user in userList)
        //    {
        //        var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
        //        if (role == null)
        //        {
        //            user.Role = "None";
        //        }
        //        else
        //        {
        //            user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId).Name;
        //        }
        //    }

        //    return View(userList);
        //}
    }
}
