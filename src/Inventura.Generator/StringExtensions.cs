using System;

namespace Generator.Generators
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (Char.IsUpper(str[0]) == true) { str = str.Replace(str[0], char.ToLower(str[0])); return str; }

            return str;
        }
    }
}
