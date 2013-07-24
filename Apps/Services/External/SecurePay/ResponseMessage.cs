using System.Runtime.Serialization;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Namespace = "")]
    public class Status
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }

        [DataMember(Order = 1)]
        internal string statusCode
        {
            get { return StatusCode.ToString(); }
            set { StatusCode = int.Parse(value); }
        }

        [DataMember(Order = 2)]
        internal string statusDescription
        {
            get { return StatusDescription; }
            set { StatusDescription = value; }
        }
    }

    [DataContract(Namespace = "")]
    public abstract class ResponseMessage
        : Message<ResponseMessageInfo>
    {
        protected ResponseMessage(ResponseMessageInfo messageInfo, ResponseMerchantInfo merchantInfo)
            : base(messageInfo)
        {
            MerchantInfo = merchantInfo;
            Status = new Status();
        }

        protected ResponseMessage()
            : base(new ResponseMessageInfo())
        {
            MerchantInfo = new ResponseMerchantInfo();
            Status = new Status();
        }

        [DataMember(Order = 1)]
        public abstract RequestType RequestType
        {
            get;
            protected set;
        }

        [DataMember(Order = 2), Prepare, Validate]
        public ResponseMerchantInfo MerchantInfo { get; set; }

        [DataMember(Order = 3)]
        public Status Status { get; set; }

        protected void CopyTo(ResponseMessage message)
        {
            base.CopyTo(message);
            message.MerchantInfo = (ResponseMerchantInfo)MerchantInfo.Clone();
            message.Status = Status;
        }
    }
}
