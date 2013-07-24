using System.Text.RegularExpressions;

namespace LinkMe.Query.Search.Members
{
    public static class RegularExpressions
    {
        private const string AllowedNameChars = @"\w\-\'\.\`\s\?!\(\):&,;#";
        private static readonly string ValidNamePart = "[" + AllowedNameChars + "]{" + Constants.MemberSearchNameMinLength + "," + Constants.MemberSearchNameMaxLength + "}";

        public static readonly string CompleteMemberSearchNamePattern = "^" + ValidNamePart + "$";

        public static readonly Regex CompleteMemberSearchName = new Regex(CompleteMemberSearchNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}