using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Public.Controllers.Join
{
    public class JoinErrorHandler
        : StandardErrorHandler
    {
        protected override string FormatErrorMessage(ValidationError error)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, error) ?? base.FormatErrorMessage(error);
        }
    }
}
