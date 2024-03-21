using DependencyInjection.Interface;
using DependencyInjection.Model;

namespace DependencyInjection.Business_Logic
{
    /// <summary>
    /// Implementation of IEmployeeService interface methods.
    /// </summary>
    public class BLEmployee : IEmployeeService
    {
        /// <summary>
        /// Stores employee details
        /// </summary>
        private static List<EMP01> _lstEmployee;

        /// <summary>
        /// Initialize static fields
        /// </summary>
        static BLEmployee()
        {
            _lstEmployee = new List<EMP01>();
        }

        /// <summary>
        /// Creating an employee
        /// </summary>
        /// <param name="objEmployee">Employee details</param>
        /// <returns><see langword="true"/> if employee created else <see langword="false"/>.</returns>
        public bool Create(EMP01 objEmployee)
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

        /// <summary>
        /// Deleting employee using employee id from the list.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns><see langword="true"/> if employee deleted sucessfully else <see langword="false"/>.</returns>
        public bool Delete(int id)
        {
            try
            {
                _lstEmployee.RemoveAll(emp => emp.P01F01 == id);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if employee exists or not.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns><see langword="true"/> if employee exists else <see langword="false"/>.</returns>
        public bool Exists(int id)
        {
            return _lstEmployee.Any(emp => emp.P01F01 == id);
        }

        /// <summary>
        /// Get all employee details
        /// </summary>
        /// <returns>All Employee Data into list form.</returns>
        public IEnumerable<EMP01> GetAll()
        {
            return _lstEmployee;
        }

        /// <summary>
        /// Returns the employee whose id matched with given id.
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee if exist else null</returns>
        public EMP01 GetById(int id)
        {
            return _lstEmployee.FirstOrDefault(emp => emp.P01F01 == id);
        }
    }
}
