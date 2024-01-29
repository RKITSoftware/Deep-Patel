using System.Collections.Generic;

namespace GenericClassDemo
{
    /// <summary>
    /// Business logic service interface has methods which are basic methods need to implement for all controllers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBLService<T>
        where T : class
    {
        List<T> GetAllData();
        T GetData(int id);
        void Create(T data);
        void Update(T data);
        void Delete(int id);
    }
}
