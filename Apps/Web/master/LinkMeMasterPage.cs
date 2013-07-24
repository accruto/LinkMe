using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.WebControls;
using LinkMe.Apps.Asp.UI;

namespace LinkMe.Web.Master
{
    public interface ILinkMeMasterPage
        : IMasterPage
    {
        LinkMeValidationSummary ValidationSummary { get; }
        SideBarContainer SideBarContainer { get; }
        HtmlForm MainForm { get; }
        PlaceHolder NonFormPlaceHolder { get; }
        ExplicitClientIdHtmlControl FormContainer { get; }
    }
}
