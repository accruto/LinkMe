using LinkMe.Apps.Agents.Validation;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Employers.Controllers.Products
{
    public class ChooseProductValidationException
        : ValidationException
    {
    }

    public class TermsValidationError
        : ValidationError
    {
        public TermsValidationError(string name)
            : base(name)
        {
        }
    }

    public class CreditCardAuthorisationValidationError
        : ValidationError
    {
        public CreditCardAuthorisationValidationError(string name)
            : base(name)
        {
        }
    }

    public class ProductsValidationFormatter
        : StandardValidationFormatter
    {
        protected override string FormatErrorMessage(ValidationException exception)
        {
            if (exception is ChooseProductValidationException)
                return "At least one of the packs must be chosen.";
            if (exception is PurchaseUserException)
                return "The credit card could not be processed: " + exception.Message;
            return base.FormatErrorMessage(exception);
        }

        protected override string FormatErrorMessage(ValidationError error)
        {
            if (error is TermsValidationError)
                return "Please agree to the terms of use.";
            if (error is CreditCardAuthorisationValidationError)
                return "Please authorise the credit card charge.";
            return base.FormatErrorMessage(error);
        }
    }
}