using Newtonsoft.Json;
using System.Data;
using System.Reflection;

namespace PlacementCellManagementAPI.Extensions
{
    /// <summary>
    /// Provides extension methods for object conversion from DTO -> POCO and DataTable -> JSON.
    /// </summary>
    public static class ConvertExtension
    {
        #region Extension Methods

        /// <summary>
        /// Converts the DTO model to POCO Model.
        /// </summary>
        /// <typeparam name="to">POCO model.</typeparam>
        /// <param name="from">DTO model reference</param>
        /// <returns>Poco model.</returns>
        public static to Convert<to>(this object from)
        {
            Type? toType = typeof(to) ?? throw new Exception();
            to? toInstance = (to)Activator.CreateInstance(type: toType);

            // Get properties
            PropertyInfo[] fromProperties = from.GetType().GetProperties();
            PropertyInfo[] toProperties = toType.GetProperties();

            foreach (PropertyInfo fromProperty in fromProperties)
            {
                PropertyInfo? toProperty = Array.Find(array: toProperties, p => p.Name == fromProperty.Name);

                if (toProperty != null && fromProperty.PropertyType == toProperty.PropertyType)
                {
                    object? value = fromProperty.GetValue(from);
                    toProperty.SetValue(toInstance, value);
                }
            }

            return toInstance != null ? toInstance : throw new Exception();
        }

        /// <summary>
        /// Converts the DataTable to its equivalent JSON representation.
        /// </summary>
        /// <param name="dataTable">DataTable to convert.</param>
        /// <returns>A JSON string that represents the DataTable.</returns>
        public static string ToString(this DataTable dataTable) => JsonConvert.SerializeObject(dataTable);

        #endregion
    }
}
