using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
    public partial class PageHeader
        : NavUserControl
    {
        protected UserType ActiveUserType { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ActiveUserType = LinkMePage.ActiveUserType;
        }
    }
}