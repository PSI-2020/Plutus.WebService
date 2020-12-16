using System.Linq;

namespace Plutus.WebService
{
   public static class CapitalizationExtension
    {
        public static string UppercaseFirstLetter(this string input) => !(input.Length > 0) ? input : char.ToUpper(input.First()) + input[1..];
    }
}
