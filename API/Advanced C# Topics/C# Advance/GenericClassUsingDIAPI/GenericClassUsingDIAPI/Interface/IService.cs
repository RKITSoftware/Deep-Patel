using System.Collections.Generic;

namespace GenericClassUsingDIAPI.Interface
{
    /// <summary>
    /// Generic service interface defining CRUD operations for a generic type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
        where T : class
    {
        // Method to retrieve all data of type T
        List<T> GetAllData();

        // Method to retrieve data of type T by ID
        T GetById(int id);

        // Method to insert new data of type T
        string Insert(T entity);

        // Method to update existing data of type T
        string Update(T entity);

        // Method to delete data of type T by ID
        string Delete(int id);
    }
}
