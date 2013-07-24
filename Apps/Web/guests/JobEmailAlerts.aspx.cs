using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI;

namespace LinkMe.Web.Guests
{
    public partial class JobEmailAlerts : LinkMePage
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

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            ucContent.ImageUrl = new ApplicationUrl("~/ui/images/anonymous-pages/job-alerts.jpg");
        }
    }
}
