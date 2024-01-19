using System.Collections.Generic;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Business_Logic
{
    public class BLUser
    {
        /// <summary>
        /// Create a user list with roles for authentication, authorization
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<USR01> GetUsers()
        {
            return new List<USR01>
            {
                new USR01 { UserId = 1, UserName ="deep", Password = "1234", Roles = "Admin,SuperAdmin", Email = "deeppatel@gmail.com"},
                new USR01 { UserId = 2, UserName ="jeet", Password = "1234", Roles = "Admin", Email = "jeetsorathiya@gmail.com"},
                new USR01 { UserId = 3, UserName ="prajval", Password = "1234", Roles = "User", Email = "prajvalgahine@gmail.com"},
                new USR01 { UserId = 4, UserName ="krinsi", Password = "1234", Roles = "User", Email = "krinsikayada@gmail.com"}
            };
        }
    }
}