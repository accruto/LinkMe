using LinkMe.Apps.Mocks.Services.SecurePay;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Orders.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass]
    public class MockStandardPurchasesCommandTests
        : PurchasesCommandTests
    {
        protected override IPurchasesCommand CreatePurchaseCommand(string url, string merchantId, string password, int? responseCode)
        {
            int? statusCode = null;
            string statusDescription = null;

            if (merchantId != null)
            {
                statusCode = 504;
                statusDescription = "Invalid merchant ID";
            }

            if (password != null)
            {
                // This seems to be returned from SecurePay instead.

                statusCode = 504;
                statusDescription = "Invalid merchant ID";
//                statusCode = 550;
//                statusDescription = "Invalid password";
            }

            var sendSecurePayCommand = new MockSendSecurePayCommand
            {
                Url = url,
                StatusCode = statusCode,
                StatusDescription = statusDescription,
                ResponseCode = responseCode,
            };

            return new PurchasesCommand(
                sendSecurePayCommand,
                Resolve<ILocationQuery>(),
                false,
                merchantId ?? Resolve<string>("securepay.merchantid"),
                password ?? Resolve<string>("securepay.password"),
                Resolve<string>("securepay.antifraud.merchantid"),
                Resolve<string>("securepay.antifraud.password"),
                "Australia");
        }
    }
}
