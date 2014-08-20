using System;
using System.Globalization;

namespace ViolinBtce.Dto.Helpers
{
    public class ConvertionHelper
    {
        public static TEnum FromString<TEnum>(string convertableString, out bool parsedSuccessfully) where TEnum : struct
        {
            if (convertableString == null) throw new ArgumentNullException("convertableString");

            TEnum convertedEnum;
            parsedSuccessfully = Enum.TryParse(convertableString.ToLowerInvariant(), out convertedEnum);
            return convertedEnum;

        }

        public static string ToString<TEnum>(TEnum convertableEnum)
        {
            var enumeration = Enum.GetName(typeof(TEnum), convertableEnum);

            return enumeration.ToLowerInvariant();
        }

        public static string DecimalToString(decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}