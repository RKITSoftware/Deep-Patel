using SchoolManagementAPI.Controllers;
using SchoolManagementAPI.Models;
using System.Linq;

namespace SchoolManagementAPI.Validation
{
    public class ValidateUser
    {
        internal static bool CheckUser(string username, string password)
        {
            return CLUserController.userList.Any(user => user.R01F02.Equals(username) &&
                user.R01F03.Equals(password));
        }

        internal static USR01 GetUserDetail(string username, string password)
        {
            return CLUserController.userList.Find(user => user.R01F02.Equals(username) &&
                user.R01F03.Equals(password));
        }
    }
}