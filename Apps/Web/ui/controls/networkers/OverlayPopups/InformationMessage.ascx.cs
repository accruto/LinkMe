using System;

namespace LinkMe.Web.UI.Controls.Networkers.OverlayPopups
{
	public partial class InformationMessage : LinkMeUserControl
	{
		public string MessageHtml;
		public bool IsErrorMessage;

		public InformationMessage()
		{}

		public InformationMessage(string messageHtml, bool isErrorMessage)
		{
			MessageHtml = messageHtml;
			IsErrorMessage = isErrorMessage;
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected string GetMessageHtml()
		{
			return MessageHtml;
		}

		protected string GetErrorBorderScript()
		{
			if (IsErrorMessage)
				return @"<script type='text/javascript'>
					document.getElementById('overlay-popup-content').style['border'] = '3px solid #f00';
				</script>";

			return string.Empty;
		}
	}
}