using BasicAuthAPI.Models;
using System.Collections.Generic;

namespace BasicAuthAPI.Business_Logic
{
    public class BLUser
    {
        /// <summary>
        /// Static method to get a list of sample users
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<USR01> GetUsers()
        {
            return new List<USR01>()
            {
                new USR01 { Id = 1001, UserName = "NormalUser", Password = "12345", Roles = "User", Email = "user@email.com" },
                new USR01 { Id = 1002, UserName = "AdminUser", Password = "12345", Roles = "User,Admin", Email = "user@email.com" },
                new USR01 { Id = 1003, UserName = "SuperAdminUser", Password = "12345", Roles = "User,Admin,SuperAdmin", Email = "user@email.com" }
            };
        }
    }
}