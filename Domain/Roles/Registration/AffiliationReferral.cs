using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration
{
    public class AffiliationReferral
    {
        private string _promotionCode;
        private string _referralCode;
        private string _refererUrl;

        [IsSet]
        public Guid RefereeId { get; set; }

        public string PromotionCode
        {
            get { return _promotionCode; }
            set { _promotionCode = TextUtil.TruncateForDisplay(value, Constants.PromoCodeMaxLength); }
        }

        public string ReferralCode
        {
            get { return _referralCode; }
            set { _referralCode = TextUtil.TruncateForDisplay(value, Constants.PromoCodeMaxLength); }
        }

        public string RefererUrl
        {
            get { return _refererUrl; }
            set { _refererUrl = TextUtil.TruncateForDisplay(value, Constants.UrlMaxLength); }
        }
    }
}