using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Public.Controllers.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Errors
{
    [TestClass]
    public class HomeErrorFormatterTests
        : ErrorFormatterFormatErrorMessageTests
    {
        protected override string GetDuplicateUserMessage()
        {
            return "A user account with this email address already exists. Please <a href=\"http://xxx/login\">log in</a> or <a href=\"http://xxx/password\">recover your password.</a>";
        }

        protected override IErrorHandler CreateErrorHandler()
        {
            return new HomeErrorHandler(new ReadOnlyUrl("http://xxx/login"), new ReadOnlyUrl("http://xxx/password"));
        }
    }
}
