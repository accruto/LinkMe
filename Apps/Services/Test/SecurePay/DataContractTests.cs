using System;
using LinkMe.Apps.Services.External.SecurePay;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    public abstract class DataContractTests
        : TestClass
    {
        protected const string ApiVersion = "xml-4.2";
        protected static readonly string MerchantId = Resolve<string>("securepay.merchantid");
        protected static readonly string Password = Resolve<string>("securepay.password");
        protected static readonly string AntiFraudMerchantId = Resolve<string>("securepay.antifraud.merchantid");
        protected static readonly string AntiFraudPassword = Resolve<string>("securepay.antifraud.password");

        protected static RequestMessageInfo CreateMessageInfo()
        {
            return new RequestMessageInfo
            {
                MessageId = MessageId.NewMessageId(),
                MessageTimestamp = new MessageTimestamp(DateTime.Now),
                Timeout = 60,
                ApiVersion = ApiVersion
            };
        }

        protected static RequestMerchantInfo CreateMerchantInfo(bool useAntiFraud)
        {
            return new RequestMerchantInfo
            {
                MerchantId = useAntiFraud ? AntiFraudMerchantId : MerchantId,
                Password = useAntiFraud ? AntiFraudPassword : Password
            };
        }
    }
}
