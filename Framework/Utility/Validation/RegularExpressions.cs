using System.Text.RegularExpressions;

namespace LinkMe.Framework.Utility.Validation
{
	public static class RegularExpressions
	{
	    public const string CompleteAlphaNumericPattern = "^[a-zA-Z0-9]*$";
        public static readonly Regex CompleteAlphaNumeric = new Regex(CompleteAlphaNumericPattern, RegexOptions.Compiled);
        public const string CompleteNumericPattern = "^[0-9]*$";
        public static readonly Regex CompleteNumeric = new Regex(CompleteNumericPattern, RegexOptions.Compiled);

        // Emails

        public const string EmailUserPattern = @"[A-Za-z0-9._%+-]+";
        public const string EmailDomainPattern = @"(([A-Za-z0-9]+[A-Za-z0-9.-]*[A-Za-z0-9]+)|([A-Za-z]))\.[A-Za-z]{2,4}";
        public const string EmailAddressPattern = EmailUserPattern + "@" + EmailDomainPattern;
        public const string MultipleEmailAddressesPattern = EmailAddressPattern + @"(\s*[,;]\s*" + EmailAddressPattern + @")*[,;\s]*";
        public const string CompleteEmailUserPattern = "^" + EmailUserPattern + "$";
        public const string CompleteEmailAddressPattern = "^" + EmailAddressPattern + "$";
        public const string CompleteMultipleEmailAddressesPattern = "^" + MultipleEmailAddressesPattern + "$";

        public static readonly Regex EmailAddress = new Regex(EmailAddressPattern, RegexOptions.Compiled);
        public static readonly Regex CompleteEmailUser = new Regex(CompleteEmailUserPattern, RegexOptions.Compiled);
        public static readonly Regex CompleteEmailAddress = new Regex(CompleteEmailAddressPattern, RegexOptions.Compiled);
        public static readonly Regex CompleteMultipleEmailAddresses = new Regex(CompleteMultipleEmailAddressesPattern, RegexOptions.Compiled);

	    // Url

        public const string CompleteUrlPattern = @"^([a-zA-Z]+:\/\/)?([^\s:\/]+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]*))?$";
    }
}
