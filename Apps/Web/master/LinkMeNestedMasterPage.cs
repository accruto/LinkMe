using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.WebControls;
using LinkMe.Apps.Asp.UI;

namespace LinkMe.Web.Master
{
    public class LinkMeNestedMasterPage
        : NestedMasterPage, ILinkMeMasterPage
    {
        protected new ILinkMeMasterPage Master
        {
            get { return base.Master as ILinkMeMasterPage; }
        }

        LinkMeValidationSummary ILinkMeMasterPage.ValidationSummary
        {
            get { return ValidationSummary; }
        }

        SideBarContainer ILinkMeMasterPage.SideBarContainer
        {
            get { return SideBarContainer; }
        }

        HtmlForm ILinkMeMasterPage.MainForm
        {
            get { return MainForm; }
        }

        PlaceHolder ILinkMeMasterPage.NonFormPlaceHolder
        {
            get { return NonFormPlaceHolder; }
        }

        ExplicitClientIdHtmlControl ILinkMeMasterPage.FormContainer
        {
            get { return FormContainer; }
        }

        protected virtual LinkMeValidationSummary ValidationSummary
        {
            get
            {
                var masterPage = Master;
                return masterPage == null ? null : masterPage.ValidationSummary;
            }
        }

        protected virtual SideBarContainer SideBarContainer
        {
            get
            {
                var masterPage = Master;
                return masterPage == null ? null : masterPage.SideBarContainer;
            }
        }

        protected virtual HtmlForm MainForm
        {
            get
            {
                var masterPage = Master;
                return masterPage == null ? null : masterPage.MainForm;
            }
        }

        protected virtual PlaceHolder NonFormPlaceHolder
        {
            get
            {
                var masterPage = Master;
                return masterPage == null ? null : masterPage.NonFormPlaceHolder;
            }
        }

        protected virtual ExplicitClientIdHtmlControl FormContainer
        {
            get
            {
                var masterPage = Master;
                return masterPage == null ? null : masterPage.FormContainer;
            }
        }
    }
}
