using LinkMe.WebControls;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class MemberSideBarLogin : LinkMeUserControl, ICanGrabFocus
    {
        public bool ShowJoinLinks
        {
            get { return ucLogin.ShowJoinLinks; }
            set { ucLogin.ShowJoinLinks = value; }
        }

        #region ICanGrabFocus Members

        public bool AllowGrabbingFocus
        {
            set { ucLogin.GrabFocusOnLoad = value; }
        }

        #endregion
    }
}