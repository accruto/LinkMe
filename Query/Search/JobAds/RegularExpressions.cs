using System.Text.RegularExpressions;

namespace LinkMe.Query.Search.JobAds
{
    public static class RegularExpressions
    {
        private const string AllowedNameChars = @"\w\-\'\.\`\s\?!\(\):&,;#";
        private static readonly string ValidNamePart = "[" + AllowedNameChars + "]{" + Constants.JobAdSearchNameMinLength + "," + Constants.JobAdSearchNameMaxLength + "}";

        public static readonly string CompleteJobAdSearchNamePattern = "^" + ValidNamePart + "$";

        public static readonly Regex CompleteJobAdSearchName = new Regex(CompleteJobAdSearchNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}