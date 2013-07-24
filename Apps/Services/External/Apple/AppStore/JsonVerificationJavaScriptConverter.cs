using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Presentation.Converters;

namespace LinkMe.Apps.Services.External.Apple.AppStore
{
    public class JsonVerificationJavaScriptConverter
        : JavaScriptConverter
    {
        private static readonly Type[] Types = new[] { typeof(JsonVerificationRequest), typeof(JsonVerificationResponse), typeof(VerificationReceipt) };

        public override object Deserialize(IDictionary<string, object> values, Type type, JavaScriptSerializer serializer)
        {
            if (type == typeof(JsonVerificationRequest))
                return new JsonVerificationRequestConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            if (type == typeof(JsonVerificationResponse))
                return new JsonVerificationResponseConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            if (type == typeof(VerificationReceipt))
                return new VerificationReceiptConverter().Deconvert(new JavaScriptValues(values, serializer), null);
            return null;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var values = new Dictionary<string, object>();
            if (obj is JsonVerificationRequest)
                new JsonVerificationRequestConverter().Convert((JsonVerificationRequest)obj, new JavaScriptValues(values, serializer));
            else if (obj is JsonVerificationResponse)
                new JsonVerificationResponseConverter().Convert((JsonVerificationResponse)obj, new JavaScriptValues(values, serializer));
            else if (obj is VerificationReceipt)
                new VerificationReceiptConverter().Convert((VerificationReceipt)obj, new JavaScriptValues(values, serializer));
            return values;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return Types; }
        }
    }
}
