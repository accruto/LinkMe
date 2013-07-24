using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Controllers.JobAds
{
    public class JobAdsErrorHandler
        : StandardErrorHandler
    {
        protected override string FormatErrorMessage(ValidationError error)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, error) ?? base.FormatErrorMessage(error);
        }
    }
}
