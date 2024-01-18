using GenericAPI.Interface;

namespace GenericAPI.Service
{
    public class Repository : IRepository
    {
        public string GetData(string key)
        {
            return "Hello " + key;
        }
    }
}