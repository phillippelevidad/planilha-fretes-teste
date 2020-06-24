using System.Text.RegularExpressions;

namespace ShippingSheets.Utility
{
    public static class StringExtensions
    {
        private static readonly Regex onlyNumbers = new Regex(@"\D", RegexOptions.Compiled);

        public static string OnlyNumbers(this string value) => onlyNumbers.Replace(value, "");
    }
}
