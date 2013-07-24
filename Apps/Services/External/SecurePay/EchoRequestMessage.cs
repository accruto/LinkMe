using System;
using System.Runtime.Serialization;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Name = "SecurePayMessage", Namespace = "")]
    public class EchoRequestMessage
        : RequestMessage
    {
        public EchoRequestMessage(RequestMessageInfo messageInfo, RequestMerchantInfo merchantInfo)
            : base(messageInfo, merchantInfo)
        {
        }

        public EchoRequestMessage()
        {
        }

        public override RequestType RequestType
        {
            get { return RequestType.Echo; }
            protected set { }
        }

        public override object Clone()
        {
            var clone = new EchoRequestMessage();
            CopyTo(clone);
            return clone;
        }
    }
}
