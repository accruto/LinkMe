using System;
using System.Runtime.Serialization;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    public enum RequestType
    {
        Echo,
        Payment,
    }

    [DataContract(Name = "SecurePayMessage", Namespace = "")]
    public abstract class Message<TMessageInfo>
        : ICloneable
        where TMessageInfo : ICloneable
    {
        protected Message(TMessageInfo messageInfo)
        {
            MessageInfo = messageInfo;
        }

        [DataMember(Order = 1), Prepare, Validate]
        public TMessageInfo MessageInfo { get; set; }

        public abstract object Clone();

        protected void CopyTo(Message<TMessageInfo> message)
        {
            message.MessageInfo = (TMessageInfo)MessageInfo.Clone();
        }
    }
}
