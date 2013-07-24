using System;
using System.Globalization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public class JsonVerificationRequestConverter
        : Converter<JsonVerificationRequest>
    {
        public override void Convert(JsonVerificationRequest request, ISetValues values)
        {
            if (request == null)
                return;
            values.SetValue("receipt-data", request.ReceiptData);
        }

        public override JsonVerificationRequest Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            throw new NotImplementedException();
        }
    }

    public class JsonVerificationResponseConverter
        : Converter<JsonVerificationResponse>
    {
        public override void Convert(JsonVerificationResponse response, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override JsonVerificationResponse Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var status = values.GetIntValue("status").Value;
            var exception = values.GetStringValue("exception");
            var receipt = values.GetChildValue<VerificationReceipt>("receipt");

            return new JsonVerificationResponse
            {
                Status = status,
                Exception = exception,
                Receipt = receipt,
            };

        }
    }

    public class VerificationReceiptConverter
        : Converter<VerificationReceipt>
    {
        public override void Convert(VerificationReceipt receipt, ISetValues values)
        {
            throw new NotImplementedException();
        }

        public override VerificationReceipt Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var quantityString = values.GetStringValue("quantity");
            var quantity = 0;
            if (quantityString != string.Empty)
                int.TryParse(quantityString, out quantity);

            var productId = values.GetStringValue("product_id");
            var transactionId = values.GetStringValue("transaction_id");
            var purchaseDate = ParseAppleDate(values.GetStringValue("purchase_date"));
            var originalTransactionId = values.GetStringValue("original_transaction_id");
            var originalPurchaseDate = ParseAppleDate(values.GetStringValue("original_purchase_date"));
            var appItemId = values.GetStringValue("app_item_id");
            var applicationRevision = values.GetStringValue("version_external_identifier");
            var applicationBundleIdentifier = values.GetStringValue("bid");
            var applicationVersion = values.GetStringValue("bvrs");


            return new VerificationReceipt
            {
                Quantity = quantity ,
                ProductId = productId,
                TransactionId = transactionId,
                PurchaseDate = purchaseDate,
                OriginalTransactionId = originalTransactionId,
                OriginalPurchaseDate = originalPurchaseDate,
                AppItemId = appItemId,
                ApplicationRevision = applicationRevision,
                ApplicationBundleIdentifier = applicationBundleIdentifier,
                ApplicationVersion = applicationVersion,
            };

        }

        private static DateTime? ParseAppleDate(string dateString)
        {
            var provider = CultureInfo.InvariantCulture;
            const string format = "yyyy-MM-dd HH:mm:ss 'Etc/GMT'";

            try
            {
                return DateTime.SpecifyKind(DateTime.ParseExact(dateString, format, provider), DateTimeKind.Utc).ToLocalTime();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
