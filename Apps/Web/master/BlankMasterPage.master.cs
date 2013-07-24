using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.UI;
using LinkMe.Web.Master;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.WebControls;

namespace LinkMe.Web.Master
{
    public partial class BlankMasterPage
        : EmptyMasterPage, ILinkMeMasterPage
    {
        LinkMeValidationSummary ILinkMeMasterPage.ValidationSummary
        {
            get { throw new NotSupportedException(); }
        }

        SideBarContainer ILinkMeMasterPage.SideBarContainer
        {
            get { throw new NotSupportedException(); }
        }

        HtmlForm ILinkMeMasterPage.MainForm
        {
            get { throw new NotSupportedException(); }
        }

        PlaceHolder ILinkMeMasterPage.NonFormPlaceHolder
        {
            get { throw new NotSupportedException(); }
        }

        ExplicitClientIdHtmlControl ILinkMeMasterPage.FormContainer
        {
            get { throw new NotSupportedException(); }
        }
    }
}
