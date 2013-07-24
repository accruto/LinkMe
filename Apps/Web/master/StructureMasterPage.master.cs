using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Web.UI;
using LinkMe.Web.UI.Controls.Common;
using LinkMe.WebControls;

namespace LinkMe.Web.Master
{
    public partial class StructureMasterPage : LinkMeNestedMasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            wcFormContainer.ID = "form-container";
        }

        protected override LinkMeValidationSummary ValidationSummary
        {
            get { return wcValidationSummary; }
        }

        protected override SideBarContainer SideBarContainer
        {
            get { return ucSidebarContainer; }
        }

        protected override HtmlForm MainForm
        {
            get { return Form; }
        }

        protected override PlaceHolder NonFormPlaceHolder
        {
            get { return phNonFormContent; }
        }

        protected override ExplicitClientIdHtmlControl FormContainer
        {
            get { return wcFormContainer; }
        }

        public void HideHeader()
        {
            ucHeader.Visible = false;
        }

        protected string ApplicationPath
        {
            get { return ((LinkMePage) Page).ApplicationPath; }
        }
    }
}
