using System.Runtime.Serialization;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Name = "SecurePayMessage", Namespace = "")]
    public class EchoResponseMessage
        : ResponseMessage
    {
        public EchoResponseMessage(ResponseMessageInfo messageInfo, ResponseMerchantInfo merchantInfo)
            : base(messageInfo, merchantInfo)
        {
        }

        public EchoResponseMessage()
        {
        }

        public override RequestType RequestType
        {
            get { return RequestType.Echo; }
            protected set { }
        }

        public override object Clone()
        {
            var clone = new EchoResponseMessage();
            CopyTo(clone);
            return clone;
        }
    }
}
