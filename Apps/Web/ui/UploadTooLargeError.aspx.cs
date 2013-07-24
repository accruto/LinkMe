using System;
using System.Web;
using System.Web.UI;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.UI
{
	/// <summary>
	/// The error page displayed when the user attempts to upload a file that is too large.
	/// </summary>
	public partial class UploadTooLargeError : Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
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
			this.Load += new EventHandler(this.Page_Load);    

		}
		#endregion

        protected ReadOnlyUrl HomeUrl
		{
			get { return HttpContext.Current.GetHomeUrl(); }
		}

		protected string ApplicationPath
		{
			get { return (Request.ApplicationPath == "/" ? "" : Request.ApplicationPath); }
		}
	}
}
