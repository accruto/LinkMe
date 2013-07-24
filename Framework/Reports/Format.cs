using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Framework.Reports
{
    public static class Format
    {
        private static readonly string[] Prefixes = new [] { "O'", "D'", "Mc", "Mac" };

        public static string JoinIgnoreNull(string separator, params object[] values)
        {
            return Join(separator, values, v => v != null);
        }

        public static string JoinIgnoreNullOrEmpty(string separator, params object[] values)
        {
            return Join(separator, values, v => !string.IsNullOrEmpty(v));
        }

        public static string CapitaliseFullName(string value)
        {
            // Convert everything to lower first.

            return string.Join(" ", (from p in value.Trim().Split(' ') select Capitalise(p)).ToArray());
        }

        private static string Capitalise(string value)
        {
            // Convert everything to lower first and make the first letter upper case.

            value = value.ToLower();
            if (value.Length > 0)
                value = char.ToUpper(value[0]) + value.Substring(1);

            // Look for prefixes.

            foreach (var prefix in Prefixes)
            {
                if (CapitalisePrefix(ref value, prefix))
                    break;
            }

            return value;
        }

        private static bool CapitalisePrefix(ref string value, string prefix)
        {
            if (value.StartsWith(prefix) && value.Length > prefix.Length)
            {
                value = prefix + char.ToUpper(value[prefix.Length]) + value.Substring(prefix.Length + 1);
                return true;
            }

            return false;
        }

        private static string Join(string separator, object[] values, Func<string, bool> predicate)
        {
            var list = new List<string>();
            foreach (var value in values)
            {
                if (value != null)
                {
                    var sValue = value.ToString();
                    if (predicate(sValue))
                        list.Add(sValue);
                }
            }

            return list.Count == 0 ? string.Empty : string.Join(separator, list.ToArray());
        }
    }
}
