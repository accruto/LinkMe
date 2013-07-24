using LinkMe.Apps.Asp.Json;
using LinkMe.Apps.Asp.Json.Models;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Purchases
{
    public class VerifyModel
        : JsonRequestModel
    {
        public string EncodedTransactionReceipt { get; set; }
    }
}
