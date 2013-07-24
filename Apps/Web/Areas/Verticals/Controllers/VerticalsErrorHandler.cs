using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Verticals.Controllers
{
    public class VerticalsErrorHandler
        : StandardErrorHandler
    {
        protected override string FormatErrorMessage(ValidationError error)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, error) ?? base.FormatErrorMessage(error);
        }

        protected override string FormatErrorMessage(UserException ex)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, ex) ?? base.FormatErrorMessage(ex);
        }
    }
}
