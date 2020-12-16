using System.ComponentModel;

namespace Plutus.WebService
{
    public static class DataTypeExtension
    {
        public static string ToDescriptionString(this DataType type)
        {
            var attributes = (DescriptionAttribute[])type
               .GetType()
               .GetField(type.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
