using System;
using LinkMe.Domain.Contacts;
using LinkMe.Web.UI;

namespace LinkMe.Web
{
    public partial class ReferalTermsAndConditions : LinkMePage
    {
        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }

        protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }
    }
}
