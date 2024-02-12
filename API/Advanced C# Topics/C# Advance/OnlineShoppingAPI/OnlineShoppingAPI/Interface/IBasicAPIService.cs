using System.Collections.Generic;
using System.Net.Http;

namespace OnlineShoppingAPI.Interface
{
    internal interface IBasicAPIService<T> 
        where T : class
    {
        HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword);
        HttpResponseMessage Create(T entity);
        HttpResponseMessage CreateFromList(List<T> entity);
        HttpResponseMessage Delete(int id);
        List<T> GetAll();
        HttpResponseMessage Update(T entity);
    }
}
