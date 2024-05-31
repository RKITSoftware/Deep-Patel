using System.Reflection;

namespace Mail_API.Extensions
{
    public static class ConvertExtension
    {
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
    }
}
