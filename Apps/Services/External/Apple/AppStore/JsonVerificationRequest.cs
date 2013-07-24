using System.Runtime.Serialization;

namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public class JsonVerificationRequest
    {
        [DataMember(Name = "receipt-data")]
        public string ReceiptData {get; set;}
    }
}
