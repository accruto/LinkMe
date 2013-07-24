using LinkMe.Apps.Presentation.Errors;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Controllers.Home
{
    public class HomeErrorHandler
        : StandardErrorHandler
    {
        private readonly ReadOnlyUrl _loginUrl;
        private readonly ReadOnlyUrl _newPasswordUrl;

        public HomeErrorHandler(ReadOnlyUrl loginUrl, ReadOnlyUrl newPasswordUrl)
        {
            _loginUrl = loginUrl;
            _newPasswordUrl = newPasswordUrl;
        }

        protected override string FormatErrorMessage(UserException ex)
        {
            return FormatErrorMessage(Errors.ResourceManager, Errors.Culture, ex.GetType(), null, new object[] { _loginUrl.ToString(), _newPasswordUrl.ToString() }) ?? base.FormatErrorMessage(ex);
        }
    }
}
