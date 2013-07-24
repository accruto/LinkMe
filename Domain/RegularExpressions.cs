using System.Text.RegularExpressions;

namespace LinkMe.Domain
{
    public static class RegularExpressions
    {
        // Name

        private const string AllowedNameChars = @"\w\-\'\.\`\s";
        private static readonly string ValidNamePart = "[" + AllowedNameChars + "]{" + Constants.NameMinLength + "," + Constants.NameMaxLength + "}";

        public static readonly string CompleteFirstNamePattern = "^" + ValidNamePart + "$";
        public static readonly string CompleteLastNamePattern = "^" + ValidNamePart + "$";
        public static readonly string CompleteFullNamePattern = "^(" + ValidNamePart + @"\s+)+?" + ValidNamePart + "$";
        public const string DisallowedNameCharPattern = "[^" + AllowedNameChars + "]";

        public static readonly Regex CompleteFirstName = new Regex(CompleteFirstNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static readonly Regex CompleteLastName = new Regex(CompleteLastNamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        // Phone number

        public static readonly string CompletePhoneNumberPattern = @"^[\d\-\+\(\)\s]{" + Constants.PhoneNumberMinLength + "," + Constants.PhoneNumberMaxLength + "}$";
        public static readonly Regex CompletePhoneNumber = new Regex(CompletePhoneNumberPattern, RegexOptions.Compiled);

        // Salary

        // "$" by itself is valid and treated the same way as empty string.
        public const string SalaryPattern = @"^\s*\$?([\d,]{1,9}(\.\d{0,2})?(k|K)?)?\s*$";
    }
}
