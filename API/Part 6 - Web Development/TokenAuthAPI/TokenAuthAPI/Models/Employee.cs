using System;
using System.Collections.Generic;

namespace TokenAuthAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public static List<Employee> GetEmployees()
        {
            return new List<Employee>()
            {
                new Employee{Id = 1, FirstName = "Deep", LastName= "Patel", Email="deeppatel@gmail.com", Gender = "Male"},
                new Employee{Id = 2, FirstName = "Jeet", LastName= "Sorathiya", Email="jeetsorathiya@gmail.com", Gender = "Male"},
                new Employee{Id = 3, FirstName = "Prajval", LastName= "Gahine", Email="prajvalgahine@gmail.com", Gender = "Male"},
                new Employee{Id = 4, FirstName = "Krinsi", LastName= "Kayada", Email="krinsikayada@gmail.com", Gender = "Feale"},
                new Employee{Id = 5, FirstName = "Prince", LastName= "Goswami", Email="princegoswami@gmail.com", Gender = "Male"}
            };
        }
    }
}