using System;
using System.Runtime.Serialization;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Services.External.SecurePay
{
    [DataContract(Namespace = "")]
    public abstract class MerchantInfo
        : ICloneable
    {
        [Required, StringLength(7, 7), AlphaNumeric]
        public string MerchantId { get; set; }

        [DataMember(Order = 1)]
        internal string merchantID
        {
            get { return MerchantId; }
            set { MerchantId = value; }
        }

        public abstract object Clone();

        protected void CopyTo(MerchantInfo merchantInfo)
        {
            merchantInfo.MerchantId = MerchantId;
        }
    }

    [DataContract(Namespace = "")]
    public class RequestMerchantInfo
        : MerchantInfo
    {
        [Required, StringLength(6, 20)]
        public string Password { get; set; }

        [DataMember(Order = 2)]
        private string password
        {
            get { return Password; }
            set { Password = value; }
        }

        public override object Clone()
        {
            var clone = new RequestMerchantInfo();
            CopyTo(clone);
            return clone;
        }

        private void CopyTo(RequestMerchantInfo merchantInfo)
        {
            base.CopyTo(merchantInfo);
            merchantInfo.Password = Password;
        }
    }

    [DataContract(Namespace = "")]
    public class ResponseMerchantInfo
        : MerchantInfo
    {
        public override object Clone()
        {
            var clone = new ResponseMerchantInfo();
            CopyTo(clone);
            return clone;
        }
    }
}
