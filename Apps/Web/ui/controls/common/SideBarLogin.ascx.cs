using LinkMe.WebControls;

namespace LinkMe.Web.UI.Controls.Common
{
    public partial class SideBarLogin : LinkMeUserControl, ICanGrabFocus, ISectionControl
    {
        public bool ShowJoinLinks
        {
            get { return ucLogin.ShowJoinLinks; }
            set { ucLogin.ShowJoinLinks = value; }
        }

        public Login LoginControl
        {
            get { return ucLogin; }
        }

        #region ICanGrabFocus Members

        public bool AllowGrabbingFocus
        {
            set { ucLogin.GrabFocusOnLoad = value; }
        }

        #endregion

        // Don't show if the user is actually logged in.
        public bool ShowSection
        {
            get { return !LoggedInUserId.HasValue; }
        }

        public string SectionTitle
        {
            get { return null; }
        }
    }
}