using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Web.Areas.Employers.Controllers.Products
{
    public class NewOrderErrorHandler
        : StandardErrorHandler
    {
        protected override string FormatErrorMessage(UserException ex)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, ex) ?? base.FormatErrorMessage(ex);
        }
    }
}
