using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NUnitAspTestPages.HtmlTester
{
	/// <summary>
	/// Summary description for HtmlInputFileTestPage.
	/// </summary>
	public partial class HtmlInputFileTestPage : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile file;
		protected System.Web.UI.WebControls.Label uploadResult;
		protected System.Web.UI.WebControls.LinkButton submit;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			uploadResult.Text = "";
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.submit.Click += new System.EventHandler(this.submit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void submit_Click(object sender, System.EventArgs e) {
			if (file.PostedFile != null &&
				file.PostedFile.FileName.Length > 0) {
				// Save the file
				string filename = file.PostedFile.FileName.Substring(
						file.PostedFile.FileName.LastIndexOf("\\")+1);
				//file.PostedFile.SaveAs(filename);
				
				uploadResult.Text = "Success";
			}
		}

	}
}
