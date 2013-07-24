using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Content;
using LinkMe.Web.UI;
using GuestContent = LinkMe.Web.Employers.Guests.Controls.Content;

namespace LinkMe.Web.Employers.Guests
{
    public partial class CandidateAlerts : LinkMePage
    {
        protected GuestContent ucContent;

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
            return UserType.Employer;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AddStyleSheetReference(StyleSheets.Employer);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ucContent.ImageUrl = new ApplicationUrl("~/ui/images/employer/email-alerts-not-logged-in.png");
        }
    }
}
