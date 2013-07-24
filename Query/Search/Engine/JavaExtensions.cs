
namespace LinkMe.Query.Search.Engine
{
    public static class JavaExtensions
    {
        public static string[] split(this string _this, string regex)
        {
            return java.lang.String.instancehelper_split(_this, regex);
        }

        public static string replaceAll(this string _this, string regex, string replacement)
        {
            return java.lang.String.instancehelper_replaceAll(_this, regex, replacement);
        }

        public static string substring(this string _this, int start, int end)
        {
            return java.lang.String.instancehelper_substring(_this, start, end);
        }
    }
}
