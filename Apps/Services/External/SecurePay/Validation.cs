using System.Text.RegularExpressions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public class ApiVersionAttribute
        : RegexAttribute
    {
        private const string Pattern = "^xml-4.2$";
        private static readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);

        public ApiVersionAttribute()
            : base(_regex)
        {
        }
    }

    public class ExpiryDateAttribute
        : RegexAttribute
    {
        private const string Pattern = "^\\d{2}/\\d{2}$";
        private static readonly Regex _regex = new Regex(Pattern, RegexOptions.Compiled);

        public ExpiryDateAttribute()
            : base(_regex)
        {
        }
    }
}
