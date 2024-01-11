using System.Collections.Generic;

namespace TokenAuthAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
        public string Email { get; set; }

        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User { UserId = 1, UserName ="deep", Password = "1234", Roles = "Admin,SuperAdmin", Email = "deeppatel@gmail.com"},
                new User { UserId = 2, UserName ="jeet", Password = "1234", Roles = "Admin", Email = "jeetsorathiya@gmail.com"},
                new User { UserId = 3, UserName ="prajval", Password = "1234", Roles = "User", Email = "prajvalgahine@gmail.com"},
                new User { UserId = 4, UserName ="krinsi", Password = "1234", Roles = "User", Email = "krinsikayada@gmail.com"}
            };
        }
    }
}