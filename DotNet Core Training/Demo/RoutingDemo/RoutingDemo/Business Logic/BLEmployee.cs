using RoutingDemo.Model;

namespace RoutingDemo.Business_Logic
{
    /// <summary>
    /// Business logic class for managing employee-related operations.
    /// </summary>
    public class BLEmployee
    {
        private static List<EMP01> _lstEmployee;

        // Static constructor to initialize the list of employees.
        static BLEmployee()
        {
            _lstEmployee = new List<EMP01>()
            {
                // Sample data initialization
                new EMP01()
                {
                    P01F01 = 1,
                    P01F02 = "Deep Patel",
                    P01F03 = 21,
                    P01F04 = "RKIT Software Pvt. Ltd."
                },
                new EMP01()
                {
                    P01F01 = 2,
                    P01F02 = "Vishal Gohil",
                    P01F03 = 22,
                    P01F04 = "Gateway Groups of Companies"
                },
                new EMP01()
                {
                    P01F01 = 3,
                    P01F02 = "Harshika Rabadiya",
                    P01F03 = 22,
                    P01F04 = "Esparkbitz Technology Pvt. Ltd."
                },
                new EMP01()
                {
                    P01F01 = 4,
                    P01F02 = "Jeet Sorathiya",
                    P01F03 = 22,
                    P01F04 = "RKIT Software Pvt. Ltd."
                },
                new EMP01()
                {
                    P01F01 = 5,
                    P01F02 = "Raj Koradiya",
                    P01F03 = 21,
                    P01F04 = "Esparkbitz Technology Pvt. Ltd."
                },
            };
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of employees.</returns>
        public List<EMP01> Get() => _lstEmployee;

        /// <summary>
        /// Retrieves an employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee to retrieve.</param>
        /// <returns>The employee object if found; otherwise, null.</returns>
        public EMP01 Get(int id) => _lstEmployee.FirstOrDefault(e => e.P01F01 == id);

        /// <summary>
        /// Retrieves employees by company name.
        /// </summary>
        /// <param name="companyName">The name of the company to filter employees by.</param>
        /// <returns>A list of employees belonging to the specified company.</returns>
        public List<EMP01> GetByCompany(string companyName)
        {
            return _lstEmployee.FindAll(e =>
                e.P01F04.Contains(companyName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="objEmployee">The employee object to add.</param>
        /// <returns>True if the employee is successfully added; otherwise, false.</returns>
        public bool Add(EMP01 objEmployee)
        {
            try
            {
                _lstEmployee.Add(objEmployee);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
