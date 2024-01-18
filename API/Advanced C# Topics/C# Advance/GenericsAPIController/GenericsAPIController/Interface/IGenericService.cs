using System.Collections.Generic;

namespace GenericsAPIController.Interface
{
    internal interface IGenericService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Delete(int id);
    }
}
