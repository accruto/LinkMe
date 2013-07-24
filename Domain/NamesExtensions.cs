using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain
{
    public static class NamesExtensions
    {
        public static string CombineLastName(this string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                return lastName;
            if (string.IsNullOrEmpty(lastName))
                return firstName;
            return firstName + " " + lastName;
        }

        public static void SplitFirstLastName(this string fullName, out string firstName, out string lastName)
        {
            // (Somewhat arbitrarily) split at first space.

            var pos = fullName.IndexOf(' ');
            firstName = pos == -1 ? string.Empty : fullName.Substring(0, pos).Trim();
            lastName = pos == -1 ? fullName.Trim() : fullName.Substring(pos + 1).Trim();
        }

        public static string MakeNamePossessive(this string name)
        {
            return name + GetNamePossessiveSuffix(name);
        }

        public static string GetNamePossessiveSuffix(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var c = name[name.Length - 1];
            return c == 's' || c == 'S' ? "'" : "'s";
        }

        public static IList<string> GetNames(this IList<string> names, string fragment)
        {
            if (string.IsNullOrEmpty(fragment) || names.IsNullOrEmpty())
                return new string[0];

            var fragmentNames = new List<string>();
            if (fragment.Contains(" "))
            {
                // Name contains spaces, need to do just search.

                foreach (var name in names)
                {
                    if (name.ToLower().Contains(fragment.ToLower()))
                        fragmentNames.Add(name);
                }

                return fragmentNames;
            }

            // Split into parts.

            foreach (var name in names)
            {
                var parts = name.Split(' ');
                var found = false;
                foreach (var part in parts)
                {
                    if (part.ToLower().StartsWith(fragment.ToLower()))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    fragmentNames.Add(name);
            }

            return fragmentNames.OrderBy(s => s).ToList();
        }
    }
}
