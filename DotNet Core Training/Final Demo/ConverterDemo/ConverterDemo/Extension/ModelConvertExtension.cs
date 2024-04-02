using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Reflection;

namespace ConverterDemo.Extension
{
    public static class ModelConvertExtension
    {
        public static T ToConvert<T>(this object obj)
        {
            PropertyInfo[] objProperties = obj.GetType().GetProperties()
                .Where(prop => prop.IsDefined(typeof(JsonPropertyAttribute), false))
                .ToArray();

            Type? TType = typeof(T);
            T? TInstance = (T?)Activator.CreateInstance(TType);

            foreach (PropertyInfo objProp in objProperties)
            {
                JsonPropertyAttribute? nameAttr = objProp.GetCustomAttribute<JsonPropertyAttribute>();
                string? name = nameAttr.PropertyName;

                PropertyInfo? TProp = TType.GetProperty(name);
                TProp.SetValue(TInstance, objProp.GetValue(obj));
            }

            return TInstance;
        }
    }

    public class BaseResponse
    {
        public bool IsError { get; set; } = false;

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }
    }

    public class Response : BaseResponse
    {
        public DataTable Data { get; set; }
    }
}
