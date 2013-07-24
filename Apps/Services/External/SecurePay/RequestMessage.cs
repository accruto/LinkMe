using System.Runtime.Serialization;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Namespace = "")]
    public abstract class RequestMessage
        : Message<RequestMessageInfo>
    {
        protected RequestMessage(RequestMessageInfo messageInfo, RequestMerchantInfo merchantInfo)
            : base(messageInfo)
        {
            MerchantInfo = merchantInfo;
        }

        protected RequestMessage()
            : base(new RequestMessageInfo())
        {
            MerchantInfo = new RequestMerchantInfo();
        }

        [DataMember(Order = 1), Prepare, Validate]
        public RequestMerchantInfo MerchantInfo { get; private set; }

        [DataMember(Order = 2)]
        public abstract RequestType RequestType
        {
            get;
            protected set;
        }

        protected void CopyTo(RequestMessage clone)
        {
            base.CopyTo(clone);
            clone.MerchantInfo = (RequestMerchantInfo)MerchantInfo.Clone();
        }
    }
}
