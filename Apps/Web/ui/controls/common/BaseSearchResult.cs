using System;
using System.Web.UI.WebControls;
using LinkMe.WebControls;

namespace LinkMe.Web.UI
{
	public abstract class BaseSearchResult : LinkMeUserControl
	{
		protected CheckBox chkSelectedRow;
		protected Table networkTable;
		protected TextBox chkSelectedRowData;

		private void Page_Load(object sender, EventArgs e)
		{
		}

		public abstract LinkMeRepeater RptResults
		{
			get; 
		}

		protected abstract Label LblMessage
		{
			get; 
		}

        protected abstract Label LblHeader
        {
            get;
        }

		public virtual string Message
		{
			get { return LblMessage.Text; }
			set { LblMessage.Text = value; }
		}

		public virtual bool ShowMessage
		{
			get { return LblMessage.Visible; }
			set { LblMessage.Visible = value; }
		}

		public string Heading
		{
			get { return LblHeader.Text; }
			set { LblHeader.Text = value; }
		}


		public void DataBind(object dataSource)
		{
			RptResults.DataSource = dataSource;
			RptResults.DataBind();
		}

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

		}

		#endregion
	}
}