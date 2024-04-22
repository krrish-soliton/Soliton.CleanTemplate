using System.Text.RegularExpressions;

namespace Soliton.Shared
{
    public static class Extensions
    {
        public static string ToSnakeCase(this string input) =>
            Regex.Replace(input, "([a-z])([A-Z])", "$1_$2").ToLower();
    }
}
