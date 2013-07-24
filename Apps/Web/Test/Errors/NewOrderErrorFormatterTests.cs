using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Web.Areas.Employers.Controllers.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Errors
{
    [TestClass]
    public class NewOrderErrorFormatterTests
        : ErrorFormatterFormatErrorMessageTests
    {
        [TestMethod]
        public void TestSecurePayFraudException()
        {
            AssertErrorMessage("Your transaction cannot be processed at this time. Please try again later or contact us directly on 1800 546563 (within Australia) or +61 2 8969 7773 (International).", new SecurePayFraudException("", "", new FraudGuard()));
        }

        protected override IErrorHandler CreateErrorHandler()
        {
            return new NewOrderErrorHandler();
        }
    }
}
