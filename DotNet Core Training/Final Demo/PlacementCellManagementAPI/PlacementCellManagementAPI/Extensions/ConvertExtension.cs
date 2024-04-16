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
        /// <typeparam name="POCO">POCO model.</typeparam>
        /// <param name="dto">DTO model reference</param>
        /// <returns>Poco model.</returns>
        public static POCO Convert<POCO>(this object dto)
        {
            Type? pocoType = typeof(POCO) ?? throw new Exception();
            POCO? pocoInstance = (POCO)Activator.CreateInstance(type: pocoType);

            // Get properties
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                PropertyInfo? pocoProperty = Array.Find(array: pocoProperties, p => p.Name == dtoProperty.Name);

                if (pocoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object? value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            return pocoInstance != null ? pocoInstance : throw new Exception();
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
