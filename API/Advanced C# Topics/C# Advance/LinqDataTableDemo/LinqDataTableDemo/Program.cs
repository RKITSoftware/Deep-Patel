using System.Data;

namespace LinqDataTableDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            EMP01 tblEmployee = new EMP01();

            // Data Adding
            tblEmployee.Rows.Add(1, "Deep Patel", 21, "Development", 50000);
            tblEmployee.Rows.Add(2, "Jeet Patel", 22, "Sales", 25000);
            tblEmployee.Rows.Add(3, "Vishal Gohil", 25, "Development", 35000);
            tblEmployee.Rows.Add(4, "Prajval Gahine", 19, "Design", 40000);
            
            // Find employee whose department is Development adn sort them according to salary in descending order.
            var query = from employee in tblEmployee.AsEnumerable()
                        where employee.Field<string>("P01F04") == "Development"
                        orderby employee.Field<int>("P01F05") descending
                        select new
                        {
                            Id = employee.Field<int>("P01F01"),
                            Name = employee.Field<string>("P01F02"),
                            Age = employee.Field<int>("P01F03"),
                            Department = employee.Field<string>("P01F04"),
                            Salary = employee.Field<int>("P01F05")
                        };

            var query2 = tblEmployee.AsEnumerable()
                .Where(row => row.Field<string>("P01F04").Equals("Development"))
                .OrderByDescending(row => row.Field<int>("P01F05"))
                .Select(row => new
                {
                    Id = row.Field<int>("P01F01"),
                    Name = row.Field<string>("P01F02"),
                    Age = row.Field<int>("P01F03"),
                    Department = row.Field<string>("P01F04"),
                    Salary = row.Field<int>("P01F05")
                }).ToList();

            // Find maximum age
            var query3 = tblEmployee.AsEnumerable().Max(row => row.Field<int>("P01F03"));

            // Return maximum age data
            var query4 = tblEmployee.AsEnumerable()
                .Where(row => row.Field<int>("P01F03") == query3)
                .Select(row => new
                {
                    Id = row.Field<int>("P01F01"),
                    Name = row.Field<string>("P01F02"),
                    Age = row.Field<int>("P01F03"),
                    Department = row.Field<string>("P01F04"),
                    Salary = row.Field<int>("P01F05")
                }).ToList();

            // Average salary of employee
            var query5 = tblEmployee.AsEnumerable().Average(row => row.Field<int>("P01F05"));

            Console.WriteLine(query5);
            foreach (var item in query4)
            {
                Console.WriteLine($"Id :- {item.Id}, Name :- {item.Name}, Salary :- {item.Salary}");
            }
        }
    }
}