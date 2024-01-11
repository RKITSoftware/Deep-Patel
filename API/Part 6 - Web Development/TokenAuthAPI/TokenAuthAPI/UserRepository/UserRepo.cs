using System;
using System.Collections.Generic;
using System.Linq;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.UserRepository
{
    public class UserRepo : IDisposable
    {
        public static List<User> userList = User.GetUsers();
        //public static List<Employee> empList = Employee.GetEmployees();

        public void Dispose()
        {
            userList.Clear();
        }

        public User ValidateUser(string username, string password)
        {
            return userList.FirstOrDefault(user => 
                user.UserName.Equals(username) && user.Password.Equals(password));
        }
    }
}