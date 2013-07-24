using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Communications;

namespace LinkMe.Web.UI.Controls.Networkers.OverlayPopups
{
    public partial class SendJobToFriend : LinkMeUserControl
    {
        public JobAdEntry JobAd { get; set; }

        protected string UsersName
        {
            get { return LoggedInUser == null ? "" : LoggedInUser.FullName; }
        }

        protected string UsersEmail
        {
            get { return LoggedInUser == null ? "" : ((ICommunicationRecipient) LoggedInUser).EmailAddress; }
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            phShowSender.Visible = LoggedInUser == null ? true : false;
            phHidenSender.Visible = !phShowSender.Visible;
        }
    }
}