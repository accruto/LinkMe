using System;
using System.Web.UI.WebControls;

namespace LinkMe.Web.UI.Controls.Common
{
	public partial class NetworkerSearchCriteria : LinkMeUserControl
	{

		
		public string FirstName
		{
			get { return txtFirstName.Text; }
		}

		public string LastName
		{
			get { return txtLastName.Text; }
		}

		public string EmailAddress
		{
			get { return txtEmailAddress.Text; }
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
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
		}
		#endregion
	}
}
