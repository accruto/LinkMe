using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Presentation.Errors
{
    public class TermsValidationError
        : ValidationError
    {
        public TermsValidationError(string name)
            : base(name)
        {
        }
    }
}
