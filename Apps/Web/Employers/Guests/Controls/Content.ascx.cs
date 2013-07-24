using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI;
using LinkMe.Web.UI.Controls.Common;

namespace LinkMe.Web.Employers.Guests.Controls
{
    public partial class Content : LinkMeUserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Don't show the login control if already logged in as an Employer (even though they
            // shouldn't normally get to a page with this control on it).
            ucLogin.Visible = Context.User.UserType() != UserType.Employer;
        }

        public string Heading { get; set; }

        public Url ImageUrl { get; set; }

        internal Login LoginControl
        {
            get { return ucLogin; }
        }
    }
}