using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Errors
{
    public class ChooseProductException
        : UserException
    {
    }

    public class CreditCardAuthorisationValidationError
        : ValidationError
    {
        public CreditCardAuthorisationValidationError(string name)
            : base(name)
        {
        }
    }
}
