namespace LinkMe.Apps.Services.Test.SecurePay
{
    public abstract class SerializationTests
        : DataContractTests
    {
        protected const string RequestMessageInfoFormat
            = "<MessageInfo><messageID>{0}</messageID><messageTimestamp>{1}</messageTimestamp><apiVersion>xml-4.2</apiVersion><timeoutValue>60</timeoutValue></MessageInfo>";

        protected static readonly string RequestMerchantInfoFormat
            = "<MerchantInfo><merchantID>{0}</merchantID><password>{1}</password></MerchantInfo>";

        protected const string ResponseMessageInfoFormat
            = "<MessageInfo><messageID>{0}</messageID><messageTimestamp>{1}</messageTimestamp><apiVersion>xml-4.2</apiVersion></MessageInfo>";

        protected static readonly string ResponseMerchantInfoFormat
            = "<MerchantInfo><merchantID>" + MerchantId + "</merchantID></MerchantInfo>";
    }
}
