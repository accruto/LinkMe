using System.Text.RegularExpressions;

namespace LinkMe.Domain.Products
{
    public static class RegularExpressions
    {
        public static readonly string CompleteCreditCardNumberPattern = @"^\d{" + Constants.CreditCardNumberMinLength + "," + Constants.CreditCardNumberMaxLength + "}$";
        public static readonly Regex CompleteCreditCardNumber = new Regex(CompleteCreditCardNumberPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static readonly string CompleteCcvPattern = @"^\d{" + Constants.CcvMinLength + "," + Constants.CcvMaxLength + "}$";
        public static readonly Regex CompleteCcv = new Regex(CompleteCcvPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }
}
