using DependencyInjection.Model;

namespace DependencyInjection.Interface
{
    public interface IEmployeeService
    {
        bool Create(EMP01 objEmployee);
        bool Delete(int id);
        /// <summary>
        /// Checks if employee exists or not.
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>true if employee exists else false.</returns>
        bool Exists(int id);
        IEnumerable<EMP01> GetAll();
        EMP01 GetById(int id);
    }
}
