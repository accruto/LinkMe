using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Products
{
    public class CreditCardNumberAttribute
        : RegexAttribute
    {
        public CreditCardNumberAttribute()
            : base(RegularExpressions.CompleteCreditCardNumber, Constants.CreditCardNumberMinLength, Constants.CreditCardNumberMaxLength)
        {
        }
    }

    public class CvvAttribute
        : RegexAttribute
    {
        public CvvAttribute()
            : base(RegularExpressions.CompleteCcv, Constants.CcvMinLength, Constants.CcvMaxLength)
        {
        }
    }
}
