using System;
using System.Reflection;

namespace VerificationDemo.Extension
{
    /// <summary>
    /// Extension class for the model converison POCO <-> DTO.
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
            Type pocoType = typeof(POCO);
            POCO pocoInstance = (POCO)Activator.CreateInstance(pocoType);

            // Get properties
            PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
            PropertyInfo[] pocoProperties = pocoType.GetProperties();

            foreach (PropertyInfo dtoProperty in dtoProperties)
            {
                PropertyInfo pocoProperty = Array.Find(pocoProperties, p => p.Name == dtoProperty.Name);

                if (pocoProperty != null && dtoProperty.PropertyType == pocoProperty.PropertyType)
                {
                    object value = dtoProperty.GetValue(dto);
                    pocoProperty.SetValue(pocoInstance, value);
                }
            }

            return pocoInstance;
        }

        #endregion Extension Methods
    }
}