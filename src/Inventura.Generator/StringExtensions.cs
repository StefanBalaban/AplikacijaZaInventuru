namespace Generator.Generators
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            if (char.IsUpper(str[0]))
            {
                str = str.Replace(str[0], char.ToLower(str[0]));
                return str;
            }

            return str;
        }
    }
}