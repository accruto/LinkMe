using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Accounts.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Errors
{
    [TestClass]
    public class ActivationErrorFormatterTests
        : ErrorFormatterFormatErrorMessageTests
    {
        [TestMethod]
        public void TestActivationCodeRequiredValidationError()
        {
            const string name = "activationCode";
            AssertErrorMessage("The activation code is required. Please log in to request another activation email.", new RequiredValidationError(name));
        }

        [TestMethod]
        public void TestVerificationCodeRequiredValidationError()
        {
            const string name = "verificationCode";
            AssertErrorMessage("The verification code is required. Please log in to request another verification email.", new RequiredValidationError(name));
        }

        [TestMethod]
        public void TestActivationCodeNotFoundValidationError()
        {
            const string name = "activationCode";
            const string value = "123";
            AssertErrorMessage("The activation code with value '" + value + "' cannot be found. Please log in to request another activation email.", new NotFoundValidationError(name, value));
        }

        [TestMethod]
        public void TestVerificationCodeNotFoundValidationError()
        {
            const string name = "verificationCode";
            const string value = "123";
            AssertErrorMessage("The verification code with value '" + value + "' cannot be found. Please log in to request another verification email.", new NotFoundValidationError(name, value));
        }

        protected override string GetDuplicateUserMessage()
        {
            return "The email address is already being used by another user.";
        }

        protected override IErrorHandler CreateErrorHandler()
        {
            return new ActivationErrorHandler();
        }
    }
}
