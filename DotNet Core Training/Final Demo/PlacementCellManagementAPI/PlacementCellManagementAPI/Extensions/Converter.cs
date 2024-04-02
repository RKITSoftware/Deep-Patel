using Newtonsoft.Json;
using System.Reflection;

namespace PlacementCellManagementAPI.Extensions
{
    /// <summary>
    /// Provides extension methods for object conversion.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Converts the properties of an object to another type with matching properties annotated with JsonPropertyAttribute.
        /// </summary>
        /// <typeparam name="T">The type to convert the object to.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>An instance of type T with properties populated from the source object.</returns>
        public static T Convert<T>(this object obj)
        {
            // Get properties of the source object that are annotated with JsonPropertyAttribute
            PropertyInfo[] objProperties = obj.GetType().GetProperties()
                .Where(prop => prop.IsDefined(typeof(JsonPropertyAttribute), false))
                .ToArray();

            Type? TType = typeof(T);
            T? TInstance = (T?)Activator.CreateInstance(TType);

            // Populate properties of the target type with values from the source object
            foreach (PropertyInfo objProp in objProperties)
            {
                JsonPropertyAttribute? nameAttr = objProp.GetCustomAttribute<JsonPropertyAttribute>();
                string? name = nameAttr.PropertyName;

                PropertyInfo? TProp = TType.GetProperty(name);

                if (TProp != null)
                {
                    TProp.SetValue(TInstance, objProp.GetValue(obj));
                }
            }

            return TInstance;
        }
    }
}
