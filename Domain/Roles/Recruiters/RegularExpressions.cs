using System.Text.RegularExpressions;

namespace LinkMe.Domain.Roles.Recruiters
{
    public static class RegularExpressions
    {
        private const string AllowedNameChars = @"\w\-\'\.\`\s\?!&()";
        private static readonly string ValidNamePart = "[" + AllowedNameChars + "]{" + Constants.OrganisationNameMinLength + "," + Constants.OrganisationNameMaxLength + "}";

        public static readonly string CompleteOrganisationNamePattern = "^" + ValidNamePart + "$";

        public static readonly Regex CompleteOrganisationName = new Regex(CompleteOrganisationNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
