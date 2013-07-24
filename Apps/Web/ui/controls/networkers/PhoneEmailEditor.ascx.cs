using LinkMe.Domain.Contacts;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class PhoneEmailEditor : LinkMeUserControl
    {
        protected string HomePhoneNumber
        {
            get
            {
                var phoneNumber = LoggedInMember.GetPhoneNumber(PhoneNumberType.Home);
                return phoneNumber == null ? null : phoneNumber.Number;
            }
        }

        protected string WorkPhoneNumber
        {
            get
            {
                var phoneNumber = LoggedInMember.GetPhoneNumber(PhoneNumberType.Work);
                return phoneNumber == null ? null : phoneNumber.Number;
            }
        }

        protected string MobilePhoneNumber
        {
            get
            {
                var phoneNumber = LoggedInMember.GetPhoneNumber(PhoneNumberType.Mobile);
                return phoneNumber == null ? null : phoneNumber.Number;
            }
        }

        protected string EmailAddress
        {
            get { return LoggedInMember.GetBestEmailAddress().Address; }
        }
    }
}