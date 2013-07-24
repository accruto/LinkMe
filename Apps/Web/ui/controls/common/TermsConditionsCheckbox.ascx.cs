using System;
using System.Web.UI.WebControls;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Utility.Validation;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.UI
{
	public partial class TermsConditionsCheckbox : LinkMeUserControl
	{
		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.valChkTermsUse.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.valChkTermsUse_ServerValidate);

		}

		#endregion

        public short TabIndex
        {
            get { return chkTermsOfUse.TabIndex; }
            set { chkTermsOfUse.TabIndex = value; }
        }

        protected static string GetTCPopupHtml()
        {
            return LinkMePage.GetPopupATag(SupportRoutes.Terms.GenerateUrl(), "TermsAndConditions", "Terms of Use", 800, 550);
        }

		private void Page_Load(object sender, EventArgs e)
		{
			valChkTermsUse.ErrorMessage = (ValidationErrorMessages.TERMS_OF_USE_NOT_AGREED_TO); 
		}

		private void valChkTermsUse_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = chkTermsOfUse.Checked;
		}
	}
}