using System;

namespace Plutus
{
   public static class DateConversionExtension
    {
        public static DateTime ConvertToDate(this int seconds) => new DateTime(1970, 1, 1).AddSeconds(seconds).ToLocalTime();
        public static int ConvertToInt(this DateTime date) => (int)date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}
