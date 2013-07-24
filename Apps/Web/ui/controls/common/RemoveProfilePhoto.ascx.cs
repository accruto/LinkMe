using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common
{
    // TODO31: Case 4333 - Integrate this into AjaxPhotoUpload. It's currently not used anywhere.
	public partial class RemoveProfilePhoto : LinkMeUserControl
	{
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        public bool RedirectPhoto;
		
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
			this.btnRemove.Click += new EventHandler(btnRemove_Click);
		}

		#endregion

        private void Page_Load(object sender, EventArgs e)
		{
            if (LoggedInMember.PhotoId != null)
            {
                btnRemove.ConfirmationText = "Are you sure you want to remove your profile photo?";
                btnRemove.ShowJavaScriptConfirmation = true;
            }
            else
            {
                btnRemove.Visible = false;
            }
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
            LoggedInMember.PhotoId = null;
            _memberAccountsCommand.UpdateMember(LoggedInMember);
		}
	}
}
